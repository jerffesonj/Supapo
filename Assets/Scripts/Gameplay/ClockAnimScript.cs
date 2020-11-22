using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockAnimScript : MonoBehaviour
{
    Animation anim;
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameplayController.instance.actualStatus == GameplayController.Status.OnRound1 
            || GameplayController.instance.actualStatus == GameplayController.Status.OnRound2 
            || GameplayController.instance.actualStatus == GameplayController.Status.OnFinalRound)
        {
            anim.enabled = true;
        }
        else
        {
            anim.enabled = false;
            image.fillAmount = 1;
        }
    }
}
