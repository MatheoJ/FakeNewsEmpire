using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using Image = UnityEngine.UI.Image;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject news_headline;
    public GameObject money_stat;
    public GameObject viewer_stat;
    public GameObject member_stat;
    public GameObject assistant;
    public GameObject deadline;
    public GameObject ban;

    public Button option1;
    public Button option2;
    public Button option3;

    string memberName = StartManager.playerName;

    public Sprite healthIcon;
    public Sprite celebrityIcon;
    public Sprite environmentIcon;

    List<Sprite> sprites = new List<Sprite>();


    //public int money = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Register Button Events
        option1.onClick.AddListener(delegate {Button1Pressed(); });
        option2.onClick.AddListener(delegate {Button2Pressed(); });
        option3.onClick.AddListener(delegate {Button3Pressed(); });

        sprites.Add(healthIcon);
        sprites.Add(celebrityIcon);
        sprites.Add(environmentIcon);
    }

    // Update is called once per frame
    void Update()
    {

    }

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
        member_stat.GetComponentInChildren<TMP_Text>().text = memberName+"ies : " + stat.Members.ToString();
        ban.GetComponentInChildren<TMP_Text>().text = stat.BanChances.ToString()+"%";

    }

    public void WriteClippy(string text)
    {
        //assistant.GetComponentInChildren<TMP_Text>().text = text;
        assistant.GetComponent<TMP_Text>().text = text; 
    }

    public void WritePosts(List<string> postText, List<int> categories)
    {
        if (postText.Count == 3)
        {
            option1.GetComponent<Image>().sprite = sprites[categories[0]];
            option2.GetComponent<Image>().sprite = sprites[categories[1]];
            option3.GetComponent<Image>().sprite = sprites[categories[2]];
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
        if (days < 10)
        {
            deadline.GetComponentInChildren<TMP_Text>().text = "0"+days.ToString();
        }
        else
        {
            deadline.GetComponentInChildren<TMP_Text>().text = days.ToString();
        }
        
    }

    public void WriteHeadline(string text)
    {
        news_headline.GetComponentInChildren<TMP_Text>().text = "BREAKING NEWS: "+text;
    }



}
