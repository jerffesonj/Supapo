using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    [SerializeField] Animator animator;

    PunchScript punchScript;

    bool onEndRound;
    void Start()
    {
        punchScript = GetComponent<PunchScript>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorController();

        if (GameplayController.instance.actualStatus == GameplayController.Status.EndRound1 ||
            GameplayController.instance.actualStatus == GameplayController.Status.EndRound2 ||
            GameplayController.instance.actualStatus == GameplayController.Status.EndFinalRound)
        {
            onEndRound = true;
        }
        else
        {
            onEndRound = false;
        }
    }

    public void AnimatorController()
    {
        animator.speed = punchScript.animSpeed;
        animator.SetInteger("punch", punchScript.punchIndex);
        animator.SetInteger("dodgedSide", punchScript.dodgedSide);
        animator.SetBool("dodged", punchScript.dodged);
        animator.SetBool("hit", punchScript.hit);
        animator.SetBool("block", punchScript.block);
        animator.SetFloat("hitTimeMultiplier", punchScript.timeHitMultiplier);
        animator.SetBool("stunned", punchScript.stunned);
        animator.SetBool("onEndRound", onEndRound);
        animator.SetBool("stayDown", punchScript.stayDown);
    }

    public bool OnEndRound { get { return onEndRound; } }

    public Animator Animator { get { return animator; } }

    public PunchScript PunchScript { get { return punchScript; } }
}
