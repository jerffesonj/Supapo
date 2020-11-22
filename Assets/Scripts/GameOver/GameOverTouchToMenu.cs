using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverTouchToMenu : MonoBehaviour
{
    [Header ("Players")]
    [SerializeField] GameObject playerModel;
    [SerializeField] GameObject carlosModel;

    [Header("Slider")]
    [SerializeField] Slider xpSlider;

    [Header("Texts")]
    [SerializeField] TMP_Text money;
    [SerializeField] TMP_Text xpLvl;
    [SerializeField] TMP_Text xpText;

    [Header("Audio Source")]
    [SerializeField] AudioSource audioSource;

    [Header("Audio Clip")]
    [SerializeField] AudioClip cheers;

    // Start is called before the first frame update
    void Start()
    {
        if (InformationGameOverScene.instance.PlayerWin)
        {
            playerModel.SetActive(true);
            carlosModel.SetActive(false);
        }
        else
        {
            playerModel.SetActive(false);
            carlosModel.SetActive(true);
        }
        StartCoroutine(XpGain());
        StartCoroutine(MoneyGain());

        audioSource.clip = cheers;
        audioSource.volume = 0.4f;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        TextMenuInformation();

    }
    private void TextMenuInformation()
    {
        xpLvl.text = PlayerAtributes.instance.PlayerLvl.ToString();
        money.text = PlayerAtributes.instance.TotalMoney.ToString();
        xpSlider.maxValue = PlayerAtributes.instance.MaxXp;
        xpSlider.value = PlayerAtributes.instance.TotalXp;
        xpText.text = PlayerAtributes.instance.TotalXp + "/" + PlayerAtributes.instance.MaxXp;
    }

    public void GoToMenu()
    {
        ChangeLoadScreenScene.instance.SetSceneName("Menu");
        InformationGameOverScene.instance.ResetVariables();
        SceneManager.LoadScene("LoadScreen");
    }
    
    public void PlayAgain()
    {
        ChangeLoadScreenScene.instance.SetSceneName("Gameplay");
        InformationGameOverScene.instance.ResetVariables();
        SceneManager.LoadScene("LoadScreen");
    }

    IEnumerator XpGain()
    {
        float totalXpToUp = InformationGameOverScene.instance.TotalValue;
        int totalXp = InformationGameOverScene.instance.TotalValue;
        while (totalXpToUp>0)
        {
            totalXpToUp -= InformationGameOverScene.instance.TotalValue/60;
            PlayerAtributes.instance.TotalXp += InformationGameOverScene.instance.TotalValue / 60;
           
            if (totalXpToUp <= 0)
            {
                //if(PlayerAtributes.instance.TotalXp != totalXp)
                //{
                //    PlayerAtributes.instance.TotalXp = totalXp;
                //}
                break;
            }

            yield return null;
        }
        
    }
    IEnumerator MoneyGain()
    {
        float totalMoneyToUp = InformationGameOverScene.instance.TotalValue;

        int totalvalue = InformationGameOverScene.instance.TotalValue + PlayerAtributes.instance.TotalMoney;
        while (totalMoneyToUp > 0)
        {
            totalMoneyToUp -= Mathf.RoundToInt(InformationGameOverScene.instance.TotalValue / 60);
            PlayerAtributes.instance.TotalMoney += Mathf.RoundToInt(InformationGameOverScene.instance.TotalValue / 60);

            if (totalMoneyToUp <= 0)
            {
                if (PlayerAtributes.instance.TotalMoney != totalvalue)
                {
                    PlayerAtributes.instance.TotalMoney = totalvalue;
                    //PlayerAtributes.instance.TotalMoney += moneyVar;
                }
                
            }

            yield return null;
        }
    }


}
