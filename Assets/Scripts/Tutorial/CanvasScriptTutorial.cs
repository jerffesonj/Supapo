using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasScriptTutorial : MonoBehaviour
{
    public HpScriptTutorial playerHpScript;
    public HpScriptTutorial enemyHpScript;

    public TMP_Text playerHp;
    public TMP_Text enemyHp;

    public TMP_Text playerStamina;
    public TMP_Text enemyStamina;

    public TMP_Text playerStun;
    public TMP_Text enemyStun;

    public Slider playerHpSlider;
    public Slider enemyHpSlider;

    public Slider playerStaminaSlider;
    public Slider enemyStaminaSlider;

    public Slider playerStunSlider;
    public Slider enemyStunSlider;

    private void Start()
    {
        playerHpSlider.maxValue = playerHpScript.MaxHp;
        playerHpSlider.value = playerHpScript.CurrentHp;

        enemyHpSlider.maxValue = enemyHpScript.MaxHp;
        enemyHpSlider.value = enemyHpScript.CurrentHp;

        playerStaminaSlider.maxValue = playerHpScript.MaxStamina;
        playerStaminaSlider.value = playerHpScript.CurrentStamina;

        enemyStaminaSlider.maxValue = enemyHpScript.MaxStamina;
        enemyStaminaSlider.value = enemyHpScript.CurrentStamina;

        playerStunSlider.maxValue = playerHpScript.MaxStun;
        playerStunSlider.value = playerHpScript.CurrentStun;

        enemyStunSlider.maxValue = enemyHpScript.MaxStun;
        enemyStunSlider.value = enemyHpScript.CurrentStun;
    }


    private void Update()
    {
        playerHpSlider.value = playerHpScript.CurrentHp;

        enemyHpSlider.value = enemyHpScript.CurrentHp;

        playerStaminaSlider.value = playerHpScript.CurrentStamina;

        enemyStaminaSlider.value = enemyHpScript.CurrentStamina;

        playerStunSlider.value = playerHpScript.CurrentStun;

        enemyStunSlider.value = enemyHpScript.CurrentStun;

        playerHp.text = Mathf.RoundToInt(playerHpScript.CurrentHp).ToString() + "/" + Mathf.RoundToInt(playerHpScript.MaxHp).ToString();
        enemyHp.text = Mathf.RoundToInt(enemyHpScript.CurrentHp).ToString() + "/" + Mathf.RoundToInt(enemyHpScript.MaxHp).ToString();

        playerStamina.text = Mathf.RoundToInt(playerHpScript.CurrentStamina).ToString() + "/" + Mathf.RoundToInt(playerHpScript.MaxStamina).ToString();
        enemyStamina.text = Mathf.RoundToInt(enemyHpScript.CurrentStamina).ToString() + "/" + Mathf.RoundToInt(enemyHpScript.MaxStamina).ToString();

        playerStun.text = Mathf.RoundToInt(playerHpScript.CurrentStun).ToString() + "/" + Mathf.RoundToInt(playerHpScript.MaxStun).ToString();
        enemyStun.text = Mathf.RoundToInt(enemyHpScript.CurrentStun).ToString() + "/" + Mathf.RoundToInt(enemyHpScript.MaxStun).ToString();
    }
}
