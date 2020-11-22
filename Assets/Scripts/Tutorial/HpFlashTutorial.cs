using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpFlashTutorial : MonoBehaviour
{
    public HpScriptTutorial hPScript;
    public Animator animator;
    public Image hpColor;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hPScript.CurrentHp <= hPScript.MaxHp * 0.25f)
        {
            animator.enabled = true;
        }
        else
        {
            animator.enabled = false;
            hpColor.color = new Color(1, 0, 0, 1);
        }
    }
}
