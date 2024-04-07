using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEditor.Build;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager UIM;
    public PostSelector postManager;

    int banChance=0;
    int money=0;
    int views=0;
    int members=0;
    int tier=0;
    int turn = 0;
    string headline;

    public int moneyCoef = 1;
    public int viewsCoef = 1;
    public int membersCoef = 1;

    string memberName;

    public Vector3 direction=Vector3.zero; //Health,Celebrity,Environment

    public List<int> healthIndexes=new List<int>();
    public List<int> celebrityIndexes = new List<int>();
    public List<int> environmentIndexes = new List<int>();
    public List<Post> postsList=new List<Post>();

    List<string> headlines=new();
    public List<Post> returnPosts = new List<Post>();

    [SerializeField]
    int maxTurn=30;

    List<int> drawnInd=new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        postsList = postManager.GetPosts();
        healthIndexes = postManager.GetHealthPosts();
        celebrityIndexes = postManager.GetCelebrityPosts();
        environmentIndexes = postManager.GetEnvironmentPosts();


        Initialize();
        
    }

    void Initialize()
    {
        returnPosts.Add(DrawGivenCrazy(1, 0));
        returnPosts.Add(DrawGivenCrazy(2, 0));
        returnPosts.Add(DrawGivenCrazy(3, 0));
        List<string> texts = new List<string>();
        for (int i = 0; i < returnPosts.Count; i++)
        {
            texts.Add(returnPosts[i].Title);
        }
        UIM.WritePosts(texts);

        UIM.WriteValues(new Stats() { Money = 0, Views = 0, BanChances = 0, Members = 0 });

        UIM.WriteHeadline("Nothing Yet");

        UIM.WriteDays(30);

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectedPost(int postNb)//updateGameState
    {
        Post current = returnPosts[postNb-1];
        drawnInd.Add(current.Id);
        headline = current.HeadLine;
        UpdateValues(current.Stats);//update numbers and turn
        UpdateDirection(current.Categories);
        
        if (BanTest())
        {
            //UIM.endscreen("banned")
            return;
        }
        if (turn >= maxTurn)
        {
            //UIM.endscreen("end")
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
        banChance += stat.BanChances;
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
        UIM.WriteValues(new Stats() { Members = members * membersCoef, Tier = tier,Money=money * moneyCoef, Views=views * viewsCoef, BanChances=banChance }); 
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
        if(Random.Range(0, 100) < banChance) { Debug.Log("banned!"); }
        return false;//Random.Range(0, 100)<banChance;
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

    Vector3 clippyLines = Vector3.zero;
    void UpdateClippy()
    {
        //draw clippy interaction depending on ban rate
        //update UI UIM.UpdateClippy(string)

        //if(money)

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
        for (int i = 0; i < returnPosts.Count; i++)
        {
            titles.Add(returnPosts[i].Title);
        }
        UIM.WritePosts(titles);


    }

    Post DrawGivenCrazy(int category,int tierGoal)
    {
        List<int> ints = new List<int>();
        for(int i = 0; i < returnPosts.Count; i++)
        {
            ints.Add(returnPosts[i].Id);
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
            } while (ints.Contains(healthIndexes[ind]) || drawnInd.Contains(healthIndexes[ind]) || !(Mathf.Abs(potentialPost.Stats.Tier - tierGoal) <= 1));


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
                } while (ints.Contains(celebrityIndexes[ind]) || drawnInd.Contains(celebrityIndexes[ind]) || !(Mathf.Abs(potentialPost.Stats.Tier - tierGoal) <= 1));

            }
            else
            {
                //draw env post
                do
                {
                    ind = Random.Range(0, environmentIndexes.Count);
                    potentialPost = postsList[environmentIndexes[ind]];
                } while (ints.Contains(environmentIndexes[ind]) || drawnInd.Contains(environmentIndexes[ind]) || !(Mathf.Abs(potentialPost.Stats.Tier - tierGoal) <= 1));
            }
        }
        return potentialPost;
    }


}
