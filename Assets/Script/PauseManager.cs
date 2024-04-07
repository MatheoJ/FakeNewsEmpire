using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{

    public GameObject pauseMenu;
    public Button resumeButton;
    public Button quitButton;
    public TMPro.TextMeshProUGUI viewCounter;


    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
            }
            else
            {
                pauseMenu.SetActive(true);
                float views = 0;
                views = GameManager.views * GameManager.viewsCoef;
                
                viewCounter.text = ""+views;
            }
        }
        
    }

    void ResumeGame()
    {
        pauseMenu.SetActive(false);
    }

    void QuitGame()
    {
        Application.Quit();
    }

}
