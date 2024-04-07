using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI MoneyText;
    public TMPro.TextMeshProUGUI MembersText;
    

    public GameObject endMenu;
    public GameObject CreditMenu;

    // Start is called before the first frame update
    void Start()
    {
        float money = GameManager.money * GameManager.moneyCoef;
        float members = GameManager.members * GameManager.membersCoef;


        MoneyText.text = "" + money;
        MembersText.text = "" + members;   
        
        endMenu.SetActive(true);
        CreditMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if right on screnn click restart
        if (Input.GetMouseButtonDown(0))
        {
            if(CreditMenu.activeSelf)
            {
                SceneManager.LoadScene("StartScene");
            }
            else
            {
                CreditMenu.SetActive(true);
                endMenu.SetActive(false);
            }
        }        
    }
}
