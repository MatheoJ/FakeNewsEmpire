using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //UIManager UIM;

    int banChance=0;
    int money=0;
    int views=0;
    int members=0;
    int turn = 0;

    string name;

    Vector3 direction=Vector3.zero;

    List<int> healthIndexes;
    List<int> celebrityIndexes;
    List<int> environmentIndexes;

    [SerializeField]
    int maxTurn=30;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectedPost(int postID)//updateGameState
    {
        UpdateValues(1,2,3,4);//update numbers
        turn += 1;
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
    void UpdateValues(int postBanChance, int postMoney, int postViews, int postMembers)
    {
        banChance = (banChance * (turn - 1) + postBanChance) * turn;
        money += postMoney;
        views += postViews;
        members += postMembers;
        //UI update
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
        //draw associated icons depending on direction
        //update UI UIM.UpdatePosts(string[3])

    }

}
