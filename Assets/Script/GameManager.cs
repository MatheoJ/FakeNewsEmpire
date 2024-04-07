using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIManager UIM;
    public PostSelector postManager;
    public BossLinesSelector bossLinesSelector;

    public float banChance=0;
    public static int money=0;
    public static int views =0;
    public static int members=0;
    int tier=0;
    public static int turn = 0;
    string headline;

    public static float moneyCoef = 17.2f;
    public static float viewsCoef = 185.1f;
    public static float membersCoef = 65.5f;
    public static float banCoef = 0.01f;



    public Vector3 direction=Vector3.zero; //Health,Celebrity,Environment

    public List<int> healthIndexes=new List<int>();
    public List<int> celebrityIndexes = new List<int>();
    public List<int> environmentIndexes = new List<int>();
    public List<Post> postsList=new List<Post>();

    List<string> headlines=new();
    public List<Post> returnPosts = new List<Post>();

    public List<BossLine> bossLines=new();

    [SerializeField]
    int maxTurn=30;

    public List<int> drawnInd=new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        postsList = postManager.GetPosts();
        healthIndexes = postManager.GetHealthPosts();
        celebrityIndexes = postManager.GetCelebrityPosts();
        environmentIndexes = postManager.GetEnvironmentPosts();
        bossLines = bossLinesSelector.GetBossLinesForTheGame();


        Initialize();
        
    }

    void Initialize()
    {
        returnPosts.Add(DrawGivenCrazy(1, 0));
        returnPosts.Add(DrawGivenCrazy(2, 0));
        returnPosts.Add(DrawGivenCrazy(3, 0));
        List<string> texts = new List<string>();
        List<int> categories = new List<int>();
        for (int i = 0; i < returnPosts.Count; i++)
        {
            texts.Add(returnPosts[i].Title);
            categories.Add(i);
        }
        UIM.WritePosts(texts, categories);

        UIM.WriteValues(new Stats() { Money = 0, Views = 0, BanChances = 0, Members = 0 });

        UIM.WriteHeadline("Nothing Yet");

        UIM.WriteDays(30);
        UIM.WriteClippy("I'm not paying you to tell the truth. But. To. Spread. These. News");
        money = 0;views = 0;members = 0; turn = 0;

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectedPost(int postNb)//updateGameState
    {
        Post current = returnPosts[postNb-1];
        drawnInd.Add(current.Id-1);
        headline = current.HeadLine;
        UpdateValues(current.Stats);//update numbers and turn
        UpdateDirection(current.Categories);
        
        if (BanTest())
        {
            //UIM.endscreen("banned")
            SceneManager.LoadScene("GameOverScene");
            return;
        }
        if (turn >= maxTurn)
        {
            //UIM.endscreen("end")
            SceneManager.LoadScene("EndScene");
            Debug.Log("30 turns reached!");
            return;
        }
        UpdateHeadline();
        UpdateClippy();
        NextPosts();
    }

    void UpdateValues(Stats stat)
    {
        //banChance = (banChance * (turn - 1) + stat.BanChances) * turn;
        banChance += stat.BanChances*banCoef;
        if(banChance < 0)
        {
            banChance = 0;
        }
        else if (banChance > 100) { banChance = 100; }
        money += stat.Money;
        if(money < 0)
        {
            money = 0;
        }
        views += stat.Views;
        if(views < 0)
        {
            views = 0;
        }
        members += stat.Members;
        if (members < 0)
        {
            members = 0;
        }
        tier = stat.Tier;
        turn += 1;
        //UI update
        UIM.WriteValues(new Stats() { Members =(int) (members * membersCoef), Tier = tier,Money=(int) (money * moneyCoef), Views=(int) (views * viewsCoef), BanChances=(int) banChance }); 
        UIM.WriteDays(30-turn);
    }

    void UpdateDirection(List<string> categories)
    {
        for(int i =0; i<categories.Count;i++)
        { 
            if (categories[i] == "Health")
            {
                direction += new Vector3(1, 0, 0);
            }
            if (categories[i] == "Celebrity")
            {
                direction += new Vector3(0, 1, 0);
            }
            if (categories[i] == "Environment")
            {
                direction += new Vector3(0, 0, 1);
            }

        }
    }


    private bool BanTest()
    {

        if (banChance < 0)
        {
            banChance = 0;
            return false;
        }
        /*if(Random.Range(0, 100) < banChance) { Debug.Log("banned!"); }
        return false;*/
        return Random.Range(0, 100)<banChance;
    }

    void UpdateHeadline()
    {
        //write headline interaction depending on direction and ban rate
        if (headlines.Count>1 && Random.Range(0, 3)== 2){
            int ind = Random.Range(1, headlines.Count);
            UIM.WriteHeadline(headlines[ind]);
        }
        if (headlines.Count > 3) { headlines.RemoveAt(0); }
        
        headlines.Add(headline);
    }

    //Vector3 clippyLines = Vector3.zero;
    void UpdateClippy()
    {
        //draw clippy interaction depending on ban rate
        //update UI UIM.UpdateClippy(string)

        //if(money)
        //bossLines;
        foreach(BossLine line in bossLines) 
        {

            if (line.Category == "Money" && line.LineOrderInCategory * 200 < money)
            {
                UIM.WriteClippy(line.Line);

                bossLines.Remove(line);
                return;
            }
            else if (line.Category == "Views" && line.LineOrderInCategory * 200 < views)
            {
                UIM.WriteClippy(line.Line);
                bossLines.Remove(line);
                return;
            }
            else if (line.Category == "Fans" && line.LineOrderInCategory * 200 < members)
            {
                UIM.WriteClippy(line.Line);
                bossLines.Remove(line);
                return;
            }
            else if (line.Category == "Bans" && line.LineOrderInCategory * 5 < banChance)
            {
                UIM.WriteClippy(line.Line);
                bossLines.Remove(line);
                return;
            }
          
        }


    }

    void NextPosts()
    {
        //draw 3 new posts depending on stats of the game
        //prepare probabilities
        List<float> probs = new List<float>();

        float max = direction.x+direction.y+direction.z;

        for(int i = 0; i < 3; i++)
        {
            probs.Add(15 + direction[i] * 55 / max); //health, celeb, env
        }

        //draw posts
        returnPosts = new List<Post>();
        for (int i = 0;i < 3; i++)
        {
            float prob = Random.value*100;
            if (prob < probs[0])
            {
                //draw health post
                returnPosts.Add(DrawGivenCrazy(1, tier));
            }
            else if (prob < probs[0]+probs[1])
            {
                //draw celeb post
                returnPosts.Add(DrawGivenCrazy(2, tier));
            }
            else
            {
                //draw env post
                returnPosts.Add(DrawGivenCrazy(3, tier));
            }
        }

        //draw associated icons depending on direction
        List<string> titles = new List<string>();
        List<int> categories = new List<int>();
        for (int i = 0; i < returnPosts.Count; i++)
        {
            titles.Add(returnPosts[i].Title);
            if (returnPosts[i].Categories[0] == "Health") { categories.Add(0); }
            else if (returnPosts[i].Categories[0] == "Celebrity") { categories.Add(1); }
            else if (returnPosts[i].Categories[0] == "Environment") { categories.Add(2); }
        }
        UIM.WritePosts(titles,categories);


    }

    Post DrawGivenCrazy(int category,int tierGoal)
    {
        int failsafe = 0;
        List<int> ints = new List<int>();
        for(int i = 0; i < returnPosts.Count; i++)
        {
            ints.Add(returnPosts[i].Id-1);
        }
        Post potentialPost=new();
        int ind = 0;
        if (category == 1)
        {
            //draw health post
            do
            {
                ind = Random.Range(0, healthIndexes.Count);
                potentialPost = postsList[healthIndexes[ind]];
                Debug.Log(healthIndexes[ind]);
                Debug.Log(drawnInd.Contains(healthIndexes[ind]).ToString());
                failsafe++;
                if(failsafe > 60)
                {
                    ind = Random.Range(0, celebrityIndexes.Count);
                    potentialPost = postsList[celebrityIndexes[ind]];
                    return potentialPost;
                }
            } while (ints.Contains(healthIndexes[ind]) || drawnInd.Contains(healthIndexes[ind]) || Mathf.Abs(potentialPost.Stats.Tier - tierGoal) > 1);


        }
        else
        {
            if (category == 2)
            {
                //draw celeb post
                do
                {
                    ind = Random.Range(0, celebrityIndexes.Count);
                    potentialPost = postsList[celebrityIndexes[ind]];

                    failsafe++;
                    if (failsafe > 60)
                    {
                        ind = Random.Range(0, environmentIndexes.Count);
                        potentialPost = postsList[environmentIndexes[ind]];
                        return potentialPost;
                    }

                } while (ints.Contains(celebrityIndexes[ind]) || drawnInd.Contains(celebrityIndexes[ind]) || Mathf.Abs(potentialPost.Stats.Tier - tierGoal) > 1);

            }
            else
            {
                //draw env post
                do
                {
                    ind = Random.Range(0, environmentIndexes.Count);
                    potentialPost = postsList[environmentIndexes[ind]];

                    failsafe++;
                    if (failsafe > 60)
                    {
                        ind = Random.Range(0, healthIndexes.Count);
                        potentialPost = postsList[healthIndexes[ind]];
                        return potentialPost;
                    }

                } while (ints.Contains(environmentIndexes[ind]) || drawnInd.Contains(environmentIndexes[ind]) || Mathf.Abs(potentialPost.Stats.Tier - tierGoal) > 1);
            }
        }
        return potentialPost;
    }


}
