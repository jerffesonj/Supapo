using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class CanvasScript : MonoBehaviour
{
    public bool setAudio = true;
    public float valueAudio;

    public AudioMixer audioMixer;

    public Toggle volumeToggle;


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

    public GameObject pausePanel;
    public GameObject goToMenuPanel;

    public Image playerRound1Win;
    public Image playerRound2Win;

    public Image enemyRound1Win;
    public Image enemyRound2Win;

    public Toggle accelerometerToggle;

    public GameObject countDown;
    public GameObject winRound;

    public TMP_Text timeText;
    

    // Start is called before the first frame update
    void Awake()
    {
        if (MenuController.instance != null)
        {
            if (MenuController.instance.UseGyro)
            {
                accelerometerToggle.isOn = true;
                MenuController.instance.UseGyro = true;
            }
            else
            {
                accelerometerToggle.isOn = false;
                MenuController.instance.UseGyro = false;
            }
        }


        
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!pausePanel.activeSelf)
            {
                Pause();
            }
            else
            {
                if (!goToMenuPanel.activeSelf)
                {
                    Pause();
                }
                else
                {
                    NaoToMenu();
                }
            }
        }

        CheckAudio();

        CanvasText();

        CanvasSliders();
    }

    void CanvasSliders()
    {
        playerHpSlider.maxValue = GameplayController.instance.PlayerHpScript.maxHP;
        playerHpSlider.value = GameplayController.instance.PlayerHpScript.currentHp;

        enemyHpSlider.maxValue = GameplayController.instance.EnemyHpScript.maxHP;
        enemyHpSlider.value = GameplayController.instance.EnemyHpScript.currentHp;

        playerStaminaSlider.maxValue = GameplayController.instance.PlayerHpScript.maxStamina;
        playerStaminaSlider.value = GameplayController.instance.PlayerHpScript.currentStamina;

        enemyStaminaSlider.maxValue = GameplayController.instance.EnemyHpScript.maxStamina;
        enemyStaminaSlider.value = GameplayController.instance.EnemyHpScript.currentStamina;

        playerStunSlider.maxValue = GameplayController.instance.PlayerHpScript.maxStun;
        playerStunSlider.value = GameplayController.instance.PlayerHpScript.currentStun;

        enemyStunSlider.maxValue = GameplayController.instance.EnemyHpScript.maxStun;
        enemyStunSlider.value = GameplayController.instance.EnemyHpScript.currentStun;
    }

    void CanvasText()
    {
        playerHp.text = Mathf.RoundToInt(GameplayController.instance.PlayerHpScript.currentHp).ToString() + "/" + Mathf.RoundToInt(GameplayController.instance.PlayerHpScript.maxHP).ToString();
        enemyHp.text = Mathf.RoundToInt(GameplayController.instance.EnemyHpScript.currentHp).ToString() + "/" + Mathf.RoundToInt(GameplayController.instance.EnemyHpScript.maxHP).ToString();

        playerStamina.text = Mathf.RoundToInt(GameplayController.instance.PlayerHpScript.currentStamina).ToString() + "/" + Mathf.RoundToInt(GameplayController.instance.PlayerHpScript.maxStamina).ToString();
        enemyStamina.text = Mathf.RoundToInt(GameplayController.instance.EnemyHpScript.currentStamina).ToString() + "/" + Mathf.RoundToInt(GameplayController.instance.EnemyHpScript.maxStamina).ToString();

        playerStun.text = Mathf.RoundToInt(GameplayController.instance.PlayerHpScript.currentStun).ToString() + "/" + Mathf.RoundToInt(GameplayController.instance.PlayerHpScript.maxStun).ToString();
        enemyStun.text = Mathf.RoundToInt(GameplayController.instance.EnemyHpScript.currentStun).ToString() + "/" + Mathf.RoundToInt(GameplayController.instance.EnemyHpScript.maxStun).ToString();
    }

    void CheckAudio()
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
    }

    public void Pause()
    {
        if (GameplayController.instance.pause)
        {
            Time.timeScale = 1;
            GameplayController.instance.pause = false;
            pausePanel.SetActive(false);
            
        }
        else
        {
            Time.timeScale = 0;
            GameplayController.instance.pause = true;
            pausePanel.SetActive(true);
        }
        SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.fx);

    }

    public void Continue()
    {
        Time.timeScale = 1;
        SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.fx);
        GameplayController.instance.pause = false;
        pausePanel.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.fx);
        ChangeLoadScreenScene.instance.SetSceneName("Gameplay");
        InformationGameOverScene.instance.ResetVariables();
        
        SceneManager.LoadScene("LoadScreen");
    }

    public void GoToMenu()
    {
        goToMenuPanel.SetActive(true);
        SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.fx);
    }

    public void SimToMenu()
    {
        Time.timeScale = 1;
        SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.fx);
        ChangeLoadScreenScene.instance.SetSceneName("Menu");
        InformationGameOverScene.instance.ResetVariables();
        
        SceneManager.LoadScene("LoadScreen");
    }

    public void NaoToMenu()
    {
        goToMenuPanel.SetActive(false);
        SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.fx);
    }

    public void UseGyro()
    {
        MenuController.instance.UseGyro = !MenuController.instance.UseGyro;
        SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.fx);
    }

    public void ChangeVolume()
    {
        setAudio = !setAudio;
        
        SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.fx);
        if (setAudio)
        {
            audioMixer.SetFloat("audioVolume", 0);
        }
        else
        {
            audioMixer.SetFloat("audioVolume", -80);
        }
    }
}
