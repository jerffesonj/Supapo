using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToggleVolumeScriptImage : MonoBehaviour
{
    [SerializeField] Image checkImage;
    [SerializeField] CanvasScript canvasScript;
    [SerializeField] SettingsMenu menuScript;
    [SerializeField] Sprite checkOnImage;
    [SerializeField] Sprite checkOffImage;

    // Update is called once per frame
    void Update()
    {
        if (menuScript != null)
        {
            if (menuScript.ValueAudio == -80)
            {
                checkImage.sprite = checkOffImage;
            }
            if (menuScript.ValueAudio == 0)
            {
                checkImage.sprite = checkOnImage;
            }
        }

        if (canvasScript != null)
        {
            {
                if (canvasScript.valueAudio == -80)
                {
                    checkImage.sprite = checkOffImage;
                }
                if (canvasScript.valueAudio == 0)
                {
                    checkImage.sprite = checkOnImage;
                }
            }
        }
    }
}
