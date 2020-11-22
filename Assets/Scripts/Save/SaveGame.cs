using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    [SerializeField] SaveController saveController;  
    
    public void Save()
    {
        StartCoroutine(saveController.ShowMessage(saveController.SavePanel,
                                                    saveController.MessageConfirmation,
                                                    saveController.MessageText, 
                                                    "Salvando", "Progresso Salvo"));

        PlayerPrefs.SetInt("PlayerLevel", PlayerAtributes.instance.PlayerLvl);
        PlayerPrefs.SetInt("PlayerMoney", PlayerAtributes.instance.TotalMoney);
        PlayerPrefs.SetInt("PlayerMaxXP", PlayerAtributes.instance.MaxXp);
        PlayerPrefs.SetInt("PlayerTotalXP", PlayerAtributes.instance.TotalXp);
        PlayerPrefs.SetInt("PlayerAtributesLvlUp", PlayerAtributes.instance.AtributesLvlUp);
        PlayerPrefs.SetInt("PlayerBoughtAtributes", PlayerAtributes.instance.BoughtAtributes);
        PlayerPrefs.SetInt("PlayerStrengh", PlayerAtributes.instance.Strengh);
        PlayerPrefs.SetInt("PlayerAgility", PlayerAtributes.instance.Agility);
        PlayerPrefs.SetInt("PlayerArmor", PlayerAtributes.instance.Armor);
        PlayerPrefs.SetInt("PlayerResistance", PlayerAtributes.instance.Resistance);

        if (MenuController.instance.Tutorial)
        {
            PlayerPrefs.SetInt("Tutorial", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Tutorial", 0);
        }

        print("salvou");
    }
    public void DeleteSave()
    {
        StartCoroutine(saveController.ShowMessage(saveController.DeletePanel, saveController.MessageConfirmation, saveController.MessageText, "Apagando", "Progresso removido"));
        PlayerPrefs.SetInt("PlayerLevel", 1);
        PlayerPrefs.SetInt("PlayerMoney", 0);
        PlayerPrefs.SetInt("PlayerMaxXP", 1000);
        PlayerPrefs.SetInt("PlayerTotalXP", 0);
        PlayerPrefs.SetInt("PlayerAtributesLvlUp", 0);
        PlayerPrefs.SetInt("PlayerBoughtAtributes", 1);
        PlayerPrefs.SetInt("PlayerStrengh", 0);
        PlayerPrefs.SetInt("PlayerAgility", 0);
        PlayerPrefs.SetInt("PlayerArmor", 0);
        PlayerPrefs.SetInt("PlayerResistance", 0);
        PlayerPrefs.SetInt("Tutorial", 1);

    }

    
    
}
