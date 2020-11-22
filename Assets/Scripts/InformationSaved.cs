using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationSaved : MonoBehaviour
{
    public static InformationSaved instance;

    int playerLevel;
    int playerMoney;
    int playerMaxXp;
    int playerTotalXp;
    int playerAtributesLvlUp;
    int playerBoughtAtributes;
    int playerStrengh;
    int playerAgility;
    int playerArmor;
    int playerResistance;
    int tutorialInt; //0 false 1 true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            CheckSave();
            SetSaveValues();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void CheckSave()
    {
        if (PlayerPrefs.GetInt("PlayerLevel") == 0)
            playerLevel = 1;
        else
            playerLevel = PlayerPrefs.GetInt("PlayerLevel");

        if (PlayerPrefs.GetInt("PlayerMoney") == 0)
            playerMoney = 0;
        else
            playerMoney = PlayerPrefs.GetInt("PlayerMoney");

        if (PlayerPrefs.GetInt("PlayerMaxXP") == 0)
            playerMaxXp = 1000;
        else
            playerMaxXp = PlayerPrefs.GetInt("PlayerMaxXP");

        if (PlayerPrefs.GetInt("PlayerTotalXP") == 0)
            playerTotalXp = 0;
        else
            playerTotalXp = PlayerPrefs.GetInt("PlayerTotalXP");

        if (PlayerPrefs.GetInt("PlayerAtributesLvlUp") == 0)
            playerAtributesLvlUp = 0;
        else
            playerAtributesLvlUp = PlayerPrefs.GetInt("PlayerAtributesLvlUp");

        if (PlayerPrefs.GetInt("PlayerBoughtAtributes") == 0)
            playerBoughtAtributes = 1;
        else
            playerBoughtAtributes = PlayerPrefs.GetInt("PlayerBoughtAtributes");

        if (PlayerPrefs.GetInt("PlayerStrengh") == 0)
            playerStrengh = 0;
        else
            playerStrengh = PlayerPrefs.GetInt("PlayerStrengh");

        if (PlayerPrefs.GetInt("PlayerAgility") == 0)
            playerAgility = 0;
        else
            playerAgility = PlayerPrefs.GetInt("PlayerAgility");

        if (PlayerPrefs.GetInt("PlayerArmor") == 0)
            playerArmor = 0;
        else
            playerArmor = PlayerPrefs.GetInt("PlayerArmor");

        if (PlayerPrefs.GetInt("PlayerResistance") == 0)
            playerResistance = 0;
        else
            playerResistance = PlayerPrefs.GetInt("PlayerResistance");

        if(PlayerPrefs.GetInt("Tutorial") == 0)
            tutorialInt = 0;
        else
            tutorialInt = 1;
    }

    void SetSaveValues()
    {
        PlayerAtributes.instance.PlayerLvl += playerLevel;
        PlayerAtributes.instance.TotalMoney += playerMoney;
        PlayerAtributes.instance.MaxXp += playerMaxXp;
        PlayerAtributes.instance.TotalXp += playerTotalXp;
        PlayerAtributes.instance.AtributesLvlUp += playerAtributesLvlUp;
        PlayerAtributes.instance.BoughtAtributes += playerBoughtAtributes;
        PlayerAtributes.instance.Strengh+= playerStrengh;
        PlayerAtributes.instance.Agility += playerAgility;
        PlayerAtributes.instance.Armor += playerArmor;
        PlayerAtributes.instance.Resistance += playerResistance;
    }
}
