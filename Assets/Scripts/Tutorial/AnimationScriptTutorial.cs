using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScriptTutorial : MonoBehaviour
{
    Animator animator;

    PunchScriptTutorial punchScript;

    private void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
        punchScript = GetComponent<PunchScriptTutorial>();
    }
    void Update()
    {
        AnimatorController();
       
    }

    public void AnimatorController()
    {
        animator.speed = punchScript.AnimSpeed;
        animator.SetInteger("punch", punchScript.PunchIndex);
        animator.SetInteger("dodgedSide", punchScript.DodgedSide);
        animator.SetBool("dodged", punchScript.Dodged);
        animator.SetBool("hit", punchScript.Hit);
        animator.SetBool("block", punchScript.Block);
        animator.SetFloat("hitTimeMultiplier", punchScript.TimeHitMultiplier);
        animator.SetBool("stunned", punchScript.Stunned);
    }
}
