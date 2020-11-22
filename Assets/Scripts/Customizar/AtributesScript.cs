using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AtributesScript : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text xpText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text strenghText;
    [SerializeField] TMP_Text agilityText;
    [SerializeField] TMP_Text armorText;
    [SerializeField] TMP_Text resistanceText;

    [SerializeField] TMP_Text gainStrengh;
    [SerializeField] TMP_Text gainAgility;
    [SerializeField] TMP_Text gainArmor;
    [SerializeField] TMP_Text gainResistance;

    [Header("Images")]
    [SerializeField] Image gainStrenghImage;
    [SerializeField] Image gainAgilityImage;
    [SerializeField] Image gainArmorImage;
    [SerializeField] Image gainResistanceImage;

    [Header("Sprites")]
    [SerializeField] Sprite levelUpImage;
    [SerializeField] Sprite moneyImage;

    [Header("Slider")]
    [SerializeField] Slider xpSlider;

    void Update()
    {
        SetTextValues();
    }
    void GainAtribute(int atribute)
    {
        SoundControllerCustom.instance.AudioSource.PlayOneShot(SoundControllerCustom.instance.SelectedFx1);

        if (PlayerAtributes.instance.AtributesLvlUp > 0)
        {
            atribute += 1;
            PlayerAtributes.instance.AtributesLvlUp -= 1;
        }
        else
        {
            if (PlayerAtributes.instance.TotalMoney >= PlayerAtributes.instance.BoughtAtributes * 100)
            {
                PlayerAtributes.instance.TotalMoney -= (PlayerAtributes.instance.BoughtAtributes) * 100;
                PlayerAtributes.instance.BoughtAtributes += 1;
                atribute += 1;
            }
        }
    }

    public void GainStrengh()
    {
        SoundControllerCustom.instance.AudioSource.PlayOneShot(SoundControllerCustom.instance.SelectedFx1);
        if (PlayerAtributes.instance.AtributesLvlUp > 0)
        {
            print("forç");
            PlayerAtributes.instance.Strengh += 1;
            PlayerAtributes.instance.AtributesLvlUp -= 1;
        }
        else
        {
            if (PlayerAtributes.instance.TotalMoney >= PlayerAtributes.instance.BoughtAtributes * 100)
            {
                PlayerAtributes.instance.TotalMoney -= (PlayerAtributes.instance.BoughtAtributes) * 100;
                PlayerAtributes.instance.BoughtAtributes += 1;
                PlayerAtributes.instance.Strengh += 1;
            }
        }
    }

    public void GainAgility()
    {
        SoundControllerCustom.instance.AudioSource.PlayOneShot(SoundControllerCustom.instance.SelectedFx1);
        if (PlayerAtributes.instance.AtributesLvlUp > 0)
        {
            PlayerAtributes.instance.Agility += 1;
            PlayerAtributes.instance.AtributesLvlUp -= 1;
        }
        else
        {
            if (PlayerAtributes.instance.TotalMoney >= PlayerAtributes.instance.BoughtAtributes * 100)
            {
                PlayerAtributes.instance.TotalMoney -= (PlayerAtributes.instance.BoughtAtributes) * 100;
                PlayerAtributes.instance.BoughtAtributes += 1;
                PlayerAtributes.instance.Agility += 1;
            }
        }
    }
    public void GainArmor()
    {
        SoundControllerCustom.instance.AudioSource.PlayOneShot(SoundControllerCustom.instance.SelectedFx1);
        if (PlayerAtributes.instance.AtributesLvlUp > 0)
        {
            PlayerAtributes.instance.Armor += 1;
            PlayerAtributes.instance.AtributesLvlUp -= 1;
        }
        else
        {
            if (PlayerAtributes.instance.TotalMoney >= PlayerAtributes.instance.BoughtAtributes * 100)
            {
                PlayerAtributes.instance.TotalMoney -= (PlayerAtributes.instance.BoughtAtributes) * 100;
                PlayerAtributes.instance.BoughtAtributes += 1;
                PlayerAtributes.instance.Armor += 1;
            }
        }
    }
    public void GainResistance()
    {
        SoundControllerCustom.instance.AudioSource.PlayOneShot(SoundControllerCustom.instance.SelectedFx1);
        //GainAtribute(PlayerAtributes.instance.Resistance);

        if (PlayerAtributes.instance.AtributesLvlUp > 0)
        {
            PlayerAtributes.instance.Resistance += 1;
            PlayerAtributes.instance.AtributesLvlUp -= 1;
        }
        else
        {
            if (PlayerAtributes.instance.TotalMoney >= PlayerAtributes.instance.BoughtAtributes * 100)
            {
                PlayerAtributes.instance.TotalMoney -= (PlayerAtributes.instance.BoughtAtributes) * 100;
                PlayerAtributes.instance.BoughtAtributes += 1;
                PlayerAtributes.instance.Resistance += 1;
            }
        }
    }

    void SetTextValues()
    {
        xpSlider.maxValue = PlayerAtributes.instance.MaxXp;
        xpSlider.value = PlayerAtributes.instance.TotalXp;

        moneyText.text = PlayerAtributes.instance.TotalMoney.ToString();
        xpText.text = PlayerAtributes.instance.TotalXp.ToString() + "/" + PlayerAtributes.instance.MaxXp.ToString();
        levelText.text = PlayerAtributes.instance.PlayerLvl.ToString();
        strenghText.text = PlayerAtributes.instance.Strengh.ToString();
        agilityText.text = PlayerAtributes.instance.Agility.ToString();
        armorText.text = PlayerAtributes.instance.Armor.ToString();
        resistanceText.text = PlayerAtributes.instance.Resistance.ToString();

        if (PlayerAtributes.instance.AtributesLvlUp > 0)
        {
            FreeValues();
        }
        else
        {
            NotFreeValues();
        }
    }

    void FreeValues()
    {
        gainStrengh.text = "Grátis";
        gainAgility.text = "Grátis";
        gainArmor.text = "Grátis";
        gainResistance.text = "Grátis";
        gainStrenghImage.sprite = levelUpImage;
        gainAgilityImage.sprite = levelUpImage;
        gainArmorImage.sprite = levelUpImage;
        gainResistanceImage.sprite = levelUpImage;
        gainStrenghImage.GetComponent<Animation>().enabled = true;
        gainAgilityImage.GetComponent<Animation>().enabled = true;
        gainArmorImage.GetComponent<Animation>().enabled = true;
        gainResistanceImage.GetComponent<Animation>().enabled = true;
    }

    void NotFreeValues()
    {
        gainStrengh.text = (PlayerAtributes.instance.BoughtAtributes * 100).ToString();
        gainAgility.text = (PlayerAtributes.instance.BoughtAtributes * 100).ToString();
        gainArmor.text = (PlayerAtributes.instance.BoughtAtributes * 100).ToString();
        gainResistance.text = (PlayerAtributes.instance.BoughtAtributes * 100).ToString();
        gainStrenghImage.sprite = moneyImage;
        gainAgilityImage.sprite = moneyImage;
        gainArmorImage.sprite = moneyImage;
        gainResistanceImage.sprite = moneyImage;
        gainStrenghImage.GetComponent<Animation>().enabled = false;
        gainAgilityImage.GetComponent<Animation>().enabled = false;
        gainArmorImage.GetComponent<Animation>().enabled = false;
        gainResistanceImage.GetComponent<Animation>().enabled = false;
    }
}
