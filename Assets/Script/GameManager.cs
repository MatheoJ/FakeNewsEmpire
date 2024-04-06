using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro.EditorUtilities;
using UnityEditor.Build;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //UIManager UIM;
    PostSelector postManager;

    int banChance=0;
    int money=0;
    int views=0;
    int members=0;
    int tier=0;
    int turn = 0;

    string name;

    Vector3 direction=Vector3.zero; //Health,Celebrity,Environment

    List<int> healthIndexes;
    List<int> celebrityIndexes;
    List<int> environmentIndexes;
    List<Post> postsList;



    [SerializeField]
    int maxTurn=30;

    // Start is called before the first frame update
    void Start()
    {
        postManager.getLists(postsList, healthIndexes, environmentIndexes, celebrityIndexes);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectedPost(int postID)//updateGameState
    {
        Post current = postsList[postID];
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
            return;
        }
        UpdateHeadline();
        UpdateClippy();
        NextPosts();
    }

    void UpdateValues(Stats stat)
    {
        banChance = (banChance * (turn - 1) + stat.BanChances) * turn;
        money += stat.Money;
        views += stat.Views;
        members += stat.Members;
        tier = stat.Tier;
        turn += 1;
        //UI update
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

        return Random.Range(0, 100)<banChance;
    }

    void UpdateHeadline()
    {

        //write headline interaction depending on direction and ban rate
    }

    void UpdateClippy()
    {
        //draw clippy interaction depending on ban rate
        //update UI UIM.UpdateClippy(string)
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
        List<Post> returnPosts = new List<Post>();
        for (int i = 0;i < 3; i++)
        {
            float prob = Random.value;
            if (prob < probs[0])
            {
                //draw health post
                //returnPosts.Add(postsList[healthIndexes[Random.Range(0,healthIndexes.Count)]]);
                returnPosts.Add(DrawGivenCrazy(1, tier));
            }
            else if (prob < probs[0]+probs[1])
            {
                //draw celeb post
                //returnPosts.Add(postsList[celebrityIndexes[Random.Range(0, healthIndexes.Count)]]);
                returnPosts.Add(DrawGivenCrazy(2, tier));
            }
            else
            {
                //draw env post
                //returnPosts.Add(postsList[environmentIndexes[Random.Range(0, healthIndexes.Count)]]);
                returnPosts.Add(DrawGivenCrazy(3, tier));
            }
        }

        //draw associated icons depending on direction
        //update UI UIM.UpdatePosts(string[3])

    }

    Post DrawGivenCrazy(int category,int tierGoal)
    {
        Post potentialPost;
        if (category==1)
        {
            //draw health post
            potentialPost = postsList[healthIndexes[Random.Range(0, healthIndexes.Count)]];
            while (Mathf.Abs(potentialPost.Stats.Tier - tierGoal)<=1)
            {
                potentialPost = postsList[healthIndexes[Random.Range(0, healthIndexes.Count)]];
            }
        }
        else if (category == 2)
        {
            //draw celeb post
            //returnPosts.Add(postsList[celebrityIndexes[Random.Range(0, healthIndexes.Count)]]);
            potentialPost = postsList[celebrityIndexes[Random.Range(0, healthIndexes.Count)]];
            while (Mathf.Abs(potentialPost.Stats.Tier - tierGoal) <= 1)
            {
                potentialPost = postsList[celebrityIndexes[Random.Range(0, healthIndexes.Count)]];
            }
        }
        else
        {
            //draw env post
            //returnPosts.Add(postsList[environmentIndexes[Random.Range(0, healthIndexes.Count)]]);
            potentialPost = postsList[environmentIndexes[Random.Range(0, healthIndexes.Count)]];
            while (Mathf.Abs(potentialPost.Stats.Tier - tierGoal) <= 1)
            {
                potentialPost = postsList[environmentIndexes[Random.Range(0, healthIndexes.Count)]];
            }
        }
        return potentialPost;
    }

}
