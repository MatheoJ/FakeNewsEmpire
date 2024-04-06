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
    public GameObject memeber_stat;
    public GameObject assistant;
    public GameObject deadline;

    public Button option1;
    public Button option2;
    public Button option3;

    public int money = 0;

    // Start is called before the first frame update
    void Start()
    {

        //Register Button Events
        option1.onClick.AddListener(delegate {ButtonPressed(option1); });
        option2.onClick.AddListener(delegate {ButtonPressed(option2); });
        option3.onClick.AddListener(delegate {ButtonPressed(option3); });
        //news_headline = GameObject.Find("");//.GetComponentInChildren<TMP_Text>().text;
        Debug.Log("LOG: " + news_headline.GetComponent<TMP_Text>().text);
        //Debug.Log("hi " + news_headline);
        //DumpToConsole(news_headline);
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void ButtonPressed(Button option)
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
    }

    void WriteValues()//int money, int banRate, int views, int members)
    {
        money_stat.GetComponentInChildren<TMP_Text>().text = "Money: " + money.ToString();
    }

    void WriteClippy(string text)
    {

    }

    void WritePosts(string[] postText)
    {

    }

    void WriteDays(int days)
    {

    }
}
