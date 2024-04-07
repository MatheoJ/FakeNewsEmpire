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

    // Start is called before the first frame update
    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        QuitButton.onClick.AddListener(QuitGame);        
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
