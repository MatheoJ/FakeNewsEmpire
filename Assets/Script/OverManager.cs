using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverManager : MonoBehaviour
{

    public TMPro.TextMeshProUGUI overText;
    public Button restartButton;
    public Button QuitButton;

    public TMPro.TextMeshProUGUI MoneyText;
    public TMPro.TextMeshProUGUI MembersText;
    public TMPro.TextMeshProUGUI DeadlineText;

    // Start is called before the first frame update
    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        QuitButton.onClick.AddListener(QuitGame);

        float money = GameManager.money * GameManager.moneyCoef;
        float members = GameManager.members * GameManager.membersCoef;
        int deadline = 30 - GameManager.turn;


        MoneyText.text = "" + money;
        MembersText.text = "" + members;
        DeadlineText.text = "" + deadline;

        string playerName = StartManager.playerName;
        if (string.IsNullOrEmpty(playerName))
        {
            overText.text = "You have been Banned !";
        }
        else
        {
            overText.text = playerName + " has been Banned!";
        }
    }

    // Update is called once per frame
    void Update()
    {

        // Check if the player has pressed the "R" key
        if(Input.GetKeyDown(KeyCode.R))
        {
            // Reload the current scene
            SceneManager.LoadScene("main_scene");

        }
      
    }
    
    void RestartGame()
    {
        Debug.Log("Restarting Game");
        SceneManager.LoadScene("main_scene");
    }

    void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
