using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject news_headline;
    public GameObject money_stat;
    public GameObject viewer_stat;
    public GameObject member_stat;
    public GameObject assistant;
    public GameObject deadline;

    public Button option1;
    public Button option2;
    public Button option3;

    public int money = 0;

    // Start is called before the first frame update
    void Start()
    {
        /*
        //Register Button Events
        option1.onClick.AddListener(delegate {ButtonPressed(option1); });
        option2.onClick.AddListener(delegate {ButtonPressed(option2); });
        option3.onClick.AddListener(delegate {ButtonPressed(option3); });*/
        //news_headline = GameObject.Find("");//.GetComponentInChildren<TMP_Text>().text;
        //Debug.Log("LOG: " + news_headline.GetComponent<TMP_Text>().text);
        //Debug.Log("hi " + news_headline);
        //DumpToConsole(news_headline);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*void ButtonPressed(Button option)
    {
        string text_to_display = option.GetComponentInChildren<TMP_Text>().text;
        switch(text_to_display){
            case "Option 1":
                Debug.Log("Clicked: " + text_to_display);
                money = money + 200;
                WriteValues();
                break;

            case "Option 2":
                Debug.Log("Clicked: " + text_to_display);
                money = money - 200;
                WriteValues();
                break;

            case "Option 3":
                Debug.Log("Clicked: " + text_to_display);
                money = money * 5;
                WriteValues();
                break;
        }
    }*/

    public void Button1Pressed()
    {
        gameManager.SelectedPost(1);
    }
    public void Button2Pressed()
    {
        gameManager.SelectedPost(2);
    }
    public void Button3Pressed()
    {
        gameManager.SelectedPost(3);
    }


    public void WriteValues(Stats stat)
    {
        money_stat.GetComponentInChildren<TMP_Text>().text = "Money: " + stat.Money.ToString();
        viewer_stat.GetComponentInChildren<TMP_Text>().text = "Viewers : " + stat.Views.ToString();
        member_stat.GetComponentInChildren<TMP_Text>().text = "Members : " + stat.Members.ToString();
    }

    public void WriteClippy(string text)
    {

    }

    public void WritePosts(List<string> postText)
    {
        if (postText.Count == 3)
        {
            option1.GetComponentInChildren<TMP_Text>().text = postText[0];
            option2.GetComponentInChildren<TMP_Text>().text = postText[1];
            option3.GetComponentInChildren<TMP_Text>().text = postText[2];
        }
        else
        {
            Debug.Log("erreur nb de posts de UIM");
        }
    }

    public void WriteDays(int days)
    {

    }

    public void WriteHeadline(string text)
    {
        news_headline.GetComponentInChildren<TMP_Text>().text = text;
    }
}
