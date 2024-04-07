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
    public GameObject assistant_img;
    public GameObject deadline;
    public GameObject ban;

    public Button option1;
    public Button option2;
    public Button option3;

    public AudioSource sfxSource_slam;
    public AudioClip sfxClip_slam;
    public AudioSource sfxSource_send;
    public AudioClip sfxClip_send;
    public AudioSource sfxSource_bgm;
    public AudioClip sfxClip_bgm;

    public float elapsed = 0.0f;
    private float shakeRange = 5.0f;
    private float shakeTime = 0.5f;
    private float scrollTime = 5.5f;
    private float scrollRate = 0.5f;
    private float shrinkTime = 1.5f;
    private float shrinkRate = 0.05f;

    private float canvasWidth = 800;

    private string assistant_text;

    
    string memberName = StartManager.playerName;

    public Sprite healthIcon;
    public Sprite celebrityIcon;
    public Sprite environmentIcon;

    List<Sprite> sprites = new List<Sprite>();


    //public int money = 0;

    // Start is called before the first frame update
    void Start()
    {
        assistant_text = assistant.GetComponentInChildren<TMP_Text>().text;
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
        elapsed += Time.deltaTime;
    }

    public void Button1Pressed()
    {
        playClip(sfxSource_send, sfxClip_send);
        StartCoroutine(ShakeEffect(assistant));
        StartCoroutine(ShakeEffect(assistant_img));
        StartCoroutine(ScrollEffect(news_headline.GetComponentInChildren<TMP_Text>()));
        StartCoroutine(ShrinkEffect(option1.gameObject));
        gameManager.SelectedPost(1);

        if (assistant_text != assistant.GetComponentInChildren<TMP_Text>().text)
        {
            StartCoroutine(ShakeEffect(assistant));
            StartCoroutine(ShakeEffect(assistant_img));
            playClip(sfxSource_slam, sfxClip_slam);
            assistant_text = assistant.GetComponentInChildren<TMP_Text>().text;
        }
    }
    public void Button2Pressed()
    {
        playClip(sfxSource_send, sfxClip_send);
        StartCoroutine(ShakeEffect(assistant));
        StartCoroutine(ShakeEffect(assistant_img));
        StartCoroutine(ScrollEffect(news_headline.GetComponentInChildren<TMP_Text>()));
        StartCoroutine(ShrinkEffect(option2.gameObject));
        gameManager.SelectedPost(2);

        if (assistant_text != assistant.GetComponentInChildren<TMP_Text>().text)
        {
            StartCoroutine(ShakeEffect(assistant));
            StartCoroutine(ShakeEffect(assistant_img));
            playClip(sfxSource_slam, sfxClip_slam);
            assistant_text = assistant.GetComponentInChildren<TMP_Text>().text;
        }
    }
    public void Button3Pressed()
    {
        playClip(sfxSource_send, sfxClip_send);
        StartCoroutine(ScrollEffect(news_headline.GetComponentInChildren<TMP_Text>()));
        StartCoroutine(ShrinkEffect(option3.gameObject));
        gameManager.SelectedPost(3);
        Debug.Log("prev: "+assistant_text);
        Debug.Log("now: "+assistant_text);

        if (assistant_text != assistant.GetComponentInChildren<TMP_Text>().text)
        {
            StartCoroutine(ShakeEffect(assistant));
            StartCoroutine(ShakeEffect(assistant_img));
            playClip(sfxSource_slam, sfxClip_slam);
            assistant_text = assistant.GetComponentInChildren<TMP_Text>().text;
        }

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

    public IEnumerator ShakeEffect(GameObject item){
        Debug.Log("entering shake effect....");
        Quaternion originalRotation = item.transform.rotation;
        elapsed = 0.0f;

        while (elapsed < shakeTime){
            float z = Random.value * shakeRange - (shakeRange/2);
            item.transform.eulerAngles = new Vector3(originalRotation.x, originalRotation.y, originalRotation.z + z); 
            yield return null;
        }

        item.transform.rotation = originalRotation;
    }

    public IEnumerator ScrollEffect(TMP_Text item){
        Debug.Log("entering scroll effect....");
        elapsed = 0.0f;

        while (elapsed < scrollTime){
            float x_pos = item.transform.position.x;
            if (x_pos < -400){
                x_pos += canvasWidth+400; 
            }
            item.transform.position = new Vector3(x_pos - scrollRate, item.transform.position.y, 
            item.transform.position.z); 
            yield return null;
        }
        //item.transform.rotation = originalRotation;
    }

    public IEnumerator ShrinkEffect(GameObject item){
        Debug.Log("entering shrink effect....");
        elapsed = 0.0f;
        Vector3 originalScale = item.transform.localScale;

        while (elapsed < shrinkTime){
            if ((item.transform.localScale.x < 0)||(item.transform.localScale.y < 0)||(item.transform.localScale.z < 0)){
                Debug.Log("DONE");
                break;
            }
            
            item.transform.localScale = new Vector3(item.transform.localScale.x-shrinkRate,
            item.transform.localScale.y-shrinkRate, item.transform.localScale.z); 
            yield return null;
        }
        item.transform.localScale = originalScale;
    }

     public void playClip(AudioSource sfxSource, AudioClip sfxClip)
    {
        sfxSource.clip = sfxClip;
        sfxSource.Play();
    }

}
