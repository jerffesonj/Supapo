using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxScriptTutorial : MonoBehaviour
{
    public BoxCollider leftBoxCollider;
    public BoxCollider rightBoxCollider;
    public PunchScriptTutorial punchScript;

    public float currentAnimSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //punchScript = this.gameObject.transform.parent.GetComponent<PunchScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DyingAnimVelocity()
    {
        currentAnimSpeed = punchScript.AnimSpeed;
        punchScript.AnimSpeed = 1;
    }

    public void GettingUpAnimSpeed()
    {
        punchScript.AnimSpeed = currentAnimSpeed;
    }
    public void ActivateLeftHitBox()
    {
        leftBoxCollider.enabled = true;
    }

    public void ActivateRightHitBox()
    {
        rightBoxCollider.enabled = true;
    }

    public void DeactivateLeftHitBox()
    {
        leftBoxCollider.enabled = false;
    }

    public void DeactivateRightHitBox()
    {
        rightBoxCollider.enabled = false;
    }

    public void HitOff()
    {

        punchScript.Hit = false;
        punchScript.TimeHitMultiplier = 1;
    }


    public void CanAttack()
    {
        punchScript.Atacou = false;
        punchScript.Block = false;
        punchScript.Dodged = false;
        punchScript.DodgedSide = 2;
    }

    public void ReturnToIdle()
    {
        punchScript.PunchIndex = 0;
    }
}
