using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxScript : MonoBehaviour
{
    [SerializeField] BoxCollider leftBoxCollider;
    [SerializeField] BoxCollider rightBoxCollider;
    [SerializeField] PunchScript punchScript;

    float currentAnimSpeed;

    public void DyingAnimVelocity()
    {
        currentAnimSpeed = punchScript.animSpeed;
        punchScript.animSpeed = 1;
    }

    public void GettingUpAnimSpeed()
    {
        punchScript.animSpeed = currentAnimSpeed;
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
        
        punchScript.hit = false;
        punchScript.timeHitMultiplier = 1;
    }


    public void CanAttack()
    {
        punchScript.atacou = false;
        punchScript.block = false;
        punchScript.dodged = false;
        punchScript.dodgedSide = 2;
    }

    public void ReturnToIdle()
    {
        punchScript.punchIndex = 0;
    }

    public BoxCollider LeftBoxCollider { get { return leftBoxCollider; } }

    public BoxCollider RightBoxCollider { get { return rightBoxCollider; } }

}
