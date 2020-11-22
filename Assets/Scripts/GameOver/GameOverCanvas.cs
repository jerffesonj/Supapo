using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverCanvas : MonoBehaviour
{
    [Header("Winner")]
    [SerializeField] TMP_Text winnerText;

    [Header("Combo")]
    [SerializeField] TMP_Text comboNumber;
    [SerializeField] TMP_Text comboXp;
    [SerializeField] TMP_Text comboGold;

    [Header("HP")]
    [SerializeField] TMP_Text hpLeftXp;
    [SerializeField] TMP_Text hpLeftGold;

    [Header("Time")]
    [SerializeField] TMP_Text timeLeftXp;
    [SerializeField] TMP_Text timeLeftGold;

    [Header("Fight")]
    [SerializeField] TMP_Text fightXP;
    [SerializeField] TMP_Text fightGold;

    [Header("XP")]
    [SerializeField] TMP_Text totalXp;
    [SerializeField] TMP_Text totalGold;
   
    private void OnLevelWasLoaded(int level)
    {
        if (level == 4)
        {
            PutInformationGameOver();
        }
    }

    private void PutInformationGameOver()
    {
        comboNumber.text = "Combo \n x " + InformationGameOverScene.instance.ComboMax.ToString();
        if (InformationGameOverScene.instance.PlayerWin)
        {
            winnerText.text = "Vencedor";
        }
        else
        {
            winnerText.text = "Perdedor";
        }
        comboXp.text = InformationGameOverScene.instance.ComboValue.ToString();
        comboGold.text = InformationGameOverScene.instance.ComboValue.ToString();
        hpLeftXp.text = InformationGameOverScene.instance.HpValue.ToString();
        hpLeftGold.text = InformationGameOverScene.instance.HpValue.ToString();
        timeLeftXp.text = InformationGameOverScene.instance.TimeValue.ToString();
        timeLeftGold.text = InformationGameOverScene.instance.TimeValue.ToString();
        fightXP.text = InformationGameOverScene.instance.FightValue.ToString();
        fightGold.text = InformationGameOverScene.instance.FightValue.ToString();
        totalXp.text = InformationGameOverScene.instance.TotalValue.ToString();
        totalGold.text = InformationGameOverScene.instance.TotalValue.ToString();
    }
}

