using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartManager : MonoBehaviour
{

    //Start button
    public Button startButton;

    //Name input field
    public TMP_InputField nameInput;

    public static string playerName;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(OnStartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This method is called when the start button is clicked
    private void OnStartGame()
    {
        // Save the entered name in the static variable
        playerName = nameInput.text;

        // Check if the player name is not empty
        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Player";
        }   

        // Load the main game scene (replace "MainScene" with the actual name of your scene)
        SceneManager.LoadScene("main_scene");
    }
}
