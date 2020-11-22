using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [Header("AudioMixer")]
    [SerializeField] AudioMixer audioMixer;

    [Header("Dropdown")]
    [SerializeField] TMP_Dropdown graphicsDropdown;

    [Header("Toggle")]
    [SerializeField] Toggle volumeToggle;
    [SerializeField] Toggle useGyro;

    [Header("Slider")]
    [SerializeField] Slider xpSlider;

    [Header("Texts")]
    [SerializeField] TMP_Text money;
    [SerializeField] TMP_Text xpLvl;
    [SerializeField] TMP_Text xpText;

    float valueAudio;
    bool setAudio = true;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.SetQualityLevel(2);
        graphicsDropdown.value = QualitySettings.GetQualityLevel();

        CheckAudioStatus();
        CheckGyroStatus();
        TextMenuInformation();

    }

    void Update()
    {
        CheckAudioStatus();
        xpSlider.value = PlayerAtributes.instance.TotalXp;
    }

    private void CheckGyroStatus()
    {
        if (MenuController.instance != null)
        {
            if (MenuController.instance.UseGyro)
            {
                useGyro.isOn = true;
                MenuController.instance.UseGyro = true;
            }
            else
            {
                useGyro.isOn = false;
                MenuController.instance.UseGyro = false;
            }
        }
    }

    private void CheckAudioStatus()
    {
        bool result = audioMixer.GetFloat("audioVolume", out valueAudio);

        if (valueAudio == -80)
        {
            volumeToggle.isOn = false;
            setAudio = false;
        }
        if (valueAudio == 0)
        {
            volumeToggle.isOn = true;
            setAudio = true;

        }
        //print(valueAudio);
    }

    private void TextMenuInformation()
    {
        xpLvl.text = PlayerAtributes.instance.PlayerLvl.ToString();
        money.text = PlayerAtributes.instance.TotalMoney.ToString();
        xpSlider.maxValue = PlayerAtributes.instance.MaxXp;
        xpSlider.value = PlayerAtributes.instance.TotalXp;
        xpText.text = PlayerAtributes.instance.TotalXp + "/" + PlayerAtributes.instance.MaxXp;
    }

    public void ChangeVolume()
    {
        MenuAudio.instance.AudioSource.PlayOneShot(MenuAudio.instance.SelectSFX);

        setAudio = !setAudio;

        if (setAudio)
        {
            audioMixer.SetFloat("audioVolume", 0);
        }
        else
        {
            audioMixer.SetFloat("audioVolume", -80);
        }
    }

    public void SetQuality(int qualityIndex)
    {
        if (MenuAudio.instance != null)
        {
            MenuAudio.instance.AudioSource.PlayOneShot(MenuAudio.instance.SelectSFX);
        }
        QualitySettings.SetQualityLevel(qualityIndex);
        print(qualityIndex);
    }

    public void UseGyro()
    {
        MenuAudio.instance.AudioSource.PlayOneShot(MenuAudio.instance.SelectSFX);
        MenuController.instance.UseGyro = !MenuController.instance.UseGyro;
    }

    public float ValueAudio { get { return valueAudio; } set { valueAudio = value; } }
}
