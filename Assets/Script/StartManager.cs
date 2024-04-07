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

    public GameObject startMenu;
    public GameObject TutorialMenu;

    public static string playerName = "Fans";

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(OnStartGame);
        TutorialMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player left clicks
        if (Input.GetMouseButtonDown(0) && TutorialMenu.activeSelf)
        {
            // Load the main game scene (replace "MainScene" with the actual name of your scene)
            SceneManager.LoadScene("main_scene");
        }
        
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
        TutorialMenu.SetActive(true);
        startMenu.SetActive(false);
    }
}
