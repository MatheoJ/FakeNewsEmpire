using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverManager : MonoBehaviour
{

    public TMPro.TextMeshProUGUI overText;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
