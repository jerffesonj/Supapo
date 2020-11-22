using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaFlash : MonoBehaviour
{
    [SerializeField] Image colorStamina;

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Flash()
    {
        animator.enabled = true;
    }

    public void TurnOffFlash()
    {
        animator.enabled = false;
        colorStamina.color = new Color(1, 1, 1, 1);
    }
}
