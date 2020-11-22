using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpFlash : MonoBehaviour
{
    [SerializeField] HPScript hPScript;
    Animator animator;
    [SerializeField] Image hpColor;
    [SerializeField] GameObject hpFlash;

    [SerializeField] bool playonce = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hPScript.currentHp<= hPScript.maxHP * 0.25f)
        {
            animator.enabled = true;
            if (hPScript.gameObject.CompareTag("Player"))
            {
                if (GameplayController.instance.actualStatus == GameplayController.Status.OnFinalRound || GameplayController.instance.actualStatus == GameplayController.Status.OnRound1 ||
                    GameplayController.instance.actualStatus == GameplayController.Status.OnRound2)
                {
                    if (GameplayController.instance.DamageBool)
                    {
                        hpFlash.SetActive(true);
                        if (playonce)
                        {
                            SoundGameplayController.instance.dangerAudioSource.clip = SoundGameplayController.instance.danger;
                            SoundGameplayController.instance.dangerAudioSource.Play();
                            playonce = false;

                        }
                    }
                }
            }
        }
        else
        {
            if (GameplayController.instance.actualStatus != GameplayController.Status.OnFinalRound || GameplayController.instance.actualStatus != GameplayController.Status.OnRound1 ||
                    GameplayController.instance.actualStatus != GameplayController.Status.OnRound2)
            {
                if (hPScript.gameObject.CompareTag("Player"))
                {
                    hpFlash.SetActive(false);
                }
            }
            animator.enabled = false;
            hpColor.color = new Color(1, 0, 0, 1);
            SoundGameplayController.instance.fxAudioSource.clip = null;
            playonce = true;
            SoundGameplayController.instance.dangerAudioSource.Stop();

        }
    }
}
