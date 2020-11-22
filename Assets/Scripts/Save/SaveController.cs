using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SaveController : MonoBehaviour
{
    [SerializeField] GameObject savePanel;
    [SerializeField] GameObject deletePanel;

    [SerializeField] GameObject messageConfirmation;
    [SerializeField] TMP_Text messageText;

    public void BackToMenu()
    {
        SoundControllerCustom.instance.AudioSource.PlayOneShot(SoundControllerCustom.instance.SelectedFx1);
        ChangeLoadScreenScene.instance.SetSceneName("Menu");
        SceneManager.LoadScene("LoadScreen");
    }

    public void GoToSavePanel()
    {
        MenuAudio.instance.AudioSource.PlayOneShot(MenuAudio.instance.SelectSFX);
        savePanel.SetActive(true);
    }
    public void ExitSavePanel()
    {
        MenuAudio.instance.AudioSource.PlayOneShot(MenuAudio.instance.SelectSFX);
        savePanel.SetActive(false);
    }
    public void GoToDeletePanel()
    {
        MenuAudio.instance.AudioSource.PlayOneShot(MenuAudio.instance.SelectSFX);
        deletePanel.SetActive(true);
    }
    public void ExitDeletePanel()
    {
        MenuAudio.instance.AudioSource.PlayOneShot(MenuAudio.instance.SelectSFX);
        deletePanel.SetActive(false);
    }

    public IEnumerator ShowMessage(GameObject panel, GameObject confirmation, TMP_Text messageText, string message1, string message2)
    {
        panel.SetActive(false);
        confirmation.SetActive(true);
        messageText.text = message1;
        yield return new WaitForSeconds(1f);
        MenuAudio.instance.AudioSource.PlayOneShot(MenuAudio.instance.SelectSFX);
        messageText.text = message2;
        yield return new WaitForSeconds(1f);
        confirmation.SetActive(false);
        ExitDeletePanel();
    }

    public GameObject SavePanel { get { return savePanel; } }
    public GameObject DeletePanel { get { return deletePanel; } }
    public GameObject MessageConfirmation{ get { return messageConfirmation; } }
    public TMP_Text MessageText { get { return messageText; } }
}
