using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject mainMenuPanel1;
    [SerializeField] GameObject mainMenuPanel2;
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] GameObject configurarPanel;
    [SerializeField] GameObject savePanel;
    [SerializeField] GameObject sairPanel;

    [Header("Texts")]
    [SerializeField] GameObject lutaText;
    [SerializeField] GameObject configText;
    [SerializeField] GameObject salvarText;
    [SerializeField] GameObject customText;
    [SerializeField] GameObject lojaText;
    [SerializeField] GameObject conquistaText;
    [SerializeField] GameObject desafioText;

    void Update()
    {
        if (MenuController.instance.OnMenu)
        {
            mainMenuPanel1.SetActive(false);
            mainMenuPanel2.SetActive(true);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (!configurarPanel.activeSelf && !savePanel.activeSelf && !sairPanel.activeSelf)
            {
                sairPanel.SetActive(true);
            }

            else if (sairPanel.activeSelf)
            {
                sairPanel.SetActive(false);
            }

            if (configurarPanel.activeSelf)
            {
                configurarPanel.SetActive(false);
            }
            if (savePanel.activeSelf)
            {
                savePanel.SetActive(false);
            }
        }
    }

    public void OnMenu()
    {
        MenuController.instance.OnMenu = true;
        mainMenuPanel1.SetActive(false);
        mainMenuPanel2.SetActive(true);
    }

    public void Campanha()
    {
        ChangeLoadScreenScene.instance.SetSceneName("Campanha");
        GoToLoadScreen();
    }

    public void Loja()
    {
    }
    public void Desafio()
    {
    }
    public void Conquista()
    {
    }

    private void NoInterationItems(GameObject localLight, GameObject localText)
    {
        MenuAudio.instance.AudioSource.PlayOneShot(MenuAudio.instance.SelectSFX);
        if (localLight.activeSelf)
        {

        }
    }

    public void LutaRapida()
    {
        MenuAudio.instance.AudioSource.PlayOneShot(MenuAudio.instance.SelectSFX);
        //if (lutaLight.activeSelf)
        {
            if (MenuController.instance.Tutorial)
            {
                print("fazer tutorial");
                tutorialPanel.SetActive(true);
            }
            else
            {
                tutorialPanel.SetActive(false);
                MenuController.instance.Tutorial = false;
                ChangeLoadScreenScene.instance.SetSceneName("Gameplay");
                GoToLoadScreen();
                print("nao");
            }
        }

        StartCoroutine(CountDown());
    }

    public void Customizar()
    {
        MenuAudio.instance.AudioSource.PlayOneShot(MenuAudio.instance.SelectSFX);
        //if (customLight.activeSelf)
        {
            ChangeLoadScreenScene.instance.SetSceneName("Customizar");
            GoToLoadScreen();
        }

        StartCoroutine(CountDown());
    }

    public void SavePanel()
    {
        MenuAudio.instance.AudioSource.PlayOneShot(MenuAudio.instance.SelectSFX);
        //if (salvarLight.activeSelf)
        {
            //ChangeLoadScreenScene.instance.SetSceneName("SaveScene");
            //GoToLoadScreen();
            savePanel.SetActive(true);

        }

        StartCoroutine(CountDown());
    }

    public void FecharSavePanel()
    {
        MenuAudio.instance.AudioSource.PlayOneShot(MenuAudio.instance.SelectSFX);
        savePanel.SetActive(false);
    }
    
    public void Configurar()
    {
        MenuAudio.instance.AudioSource.PlayOneShot(MenuAudio.instance.SelectSFX);
        //if (configLight.activeSelf)
        {
            configurarPanel.SetActive(true);
        }
        StartCoroutine(CountDown());
    }

    public void VoltarConfig()
    {
        MenuAudio.instance.AudioSource.PlayOneShot(MenuAudio.instance.SelectSFX);
        configurarPanel.SetActive(false);
    }
    public void Sair()
    {
        MenuAudio.instance.AudioSource.PlayOneShot(MenuAudio.instance.SelectSFX);
        sairPanel.SetActive(true);
    }

    public void SairSim()
    {
        MenuAudio.instance.AudioSource.PlayOneShot(MenuAudio.instance.SelectSFX);
        Application.Quit();
    }

    public void SairNao()
    {
        MenuAudio.instance.AudioSource.PlayOneShot(MenuAudio.instance.SelectSFX);
        sairPanel.SetActive(false);
    }

    void GoToLoadScreen()
    {
        
        SceneManager.LoadScene("LoadScreen");
    }

    public void SimTutorial()
    {
        MenuAudio.instance.AudioSource.PlayOneShot(MenuAudio.instance.SelectSFX);
        print("irTutorial");
        MenuController.instance.Tutorial = false;
        TutorialScene();
    }

    public void NaoTutorial()
    {
        MenuAudio.instance.AudioSource.PlayOneShot(MenuAudio.instance.SelectSFX);
        tutorialPanel.SetActive(false);
        ChangeLoadScreenScene.instance.SetSceneName("Gameplay");
        GoToLoadScreen();
        print("nao");
    }
    public void TutorialScene()
    {
        ChangeLoadScreenScene.instance.SetSceneName("Tutorial");
        GoToLoadScreen();
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
