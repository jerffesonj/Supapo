using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DodgeScriptTutorial : MonoBehaviour
{
    [SerializeField] Transform local;
    float accelerationValue;
    float time;

    float actualValueAcel;
    float lastValueAcel;
    float timeChange;

    PunchScriptTutorial punchScript;

    Vector3 acel;
    Vector3 acelVector;

    // Start is called before the first frame update
    void Start()
    {
        punchScript = GetComponent<PunchScriptTutorial>();
    }

    void Update()
    {
        PlayerDodge();
        RotatePlayerOnRing();
        PlayerInput();
    }

    private void PlayerInput()
    {
        if (CompareTag("Player"))
        {
            if (accelerationValue >= 0.4f && accelerationValue <= 0.6f)
            //if (Input.GetMouseButton(1))
            {
                if (!punchScript.Hit)
                {
                    //if (!punchScript.atacou)
                    {
                        if (!punchScript.Block)
                        {
                            if (!punchScript.Dodged)
                            {
                                if (!punchScript.Stunned)
                                {
                                    if (punchScript.HPScriptTutorial.CurrentStamina >= punchScript.StaminaDodge)
                                    {
                                        DodgedRight();
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (accelerationValue >= -0.6f && accelerationValue <= -0.4f)
            {
                if (!punchScript.Hit)
                {
                    //if (!punchScript.atacou)
                    {
                        if (!punchScript.Block)
                        {
                            if (!punchScript.Dodged)
                            {
                                if (!punchScript.Stunned)
                                {
                                    if (punchScript.HPScriptTutorial.CurrentStamina >= punchScript.StaminaDodge)
                                    {
                                        DodgedLeft();
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                //punchScript.dodged = false;
            }
        }
    }

    private void RotatePlayerOnRing()
    {
        if (punchScript.Dodged)
        {
            if (punchScript.DodgedSide == 0)
            {
                Vector3 rotationLocal;
                rotationLocal = local.rotation.eulerAngles;
                rotationLocal.y += 0.3f;
                local.rotation = Quaternion.Euler(rotationLocal);
            }
            if (punchScript.DodgedSide == 1)
            {
                Vector3 rotationLocal;
                rotationLocal = local.rotation.eulerAngles;
                rotationLocal.y -= 0.3f;
                local.rotation = Quaternion.Euler(rotationLocal);
            }
        }
    }

    private void PlayerDodge()
    {
        if (tag == ("Player"))
        {
            if (TutorialManager.instance.actualState == TutorialManager.States.Pt14 || (TutorialManager.instance.actualState == TutorialManager.States.Pt18))
            {
                //accelerationValue = Input.acceleration.x;
                acel = Input.acceleration;
                acelVector = new Vector3(12f, 0, 0);
                Vector3 vectorZero = Vector3.zero;
                Vector3.SmoothDamp(acel, acelVector, ref vectorZero, 2);
                accelerationValue = acel.x;
            }
            else if (TutorialManager.instance.actualState == TutorialManager.States.Pt16)
            {

            }
            else
            {
                punchScript.Dodged = false;
            }

        }
    }

    public void DodgedRight()
    {
        //punchScript.DodgeRight();
        if (punchScript.Atacou)
        {
            punchScript.Atacou = false;
            punchScript.PunchIndex = 0;
        }
        print("dodgeright");
        punchScript.Dodged = true;
        punchScript.DodgedSide = 1;
        punchScript.HPScriptTutorial.CurrentStamina -= punchScript.StaminaDodge;
       

    }
    public void DodgedLeft()
    {
        if (punchScript.Atacou)
        {
            punchScript.Atacou = false;
            punchScript.PunchIndex = 0;
        }
        //punchScript.DodgeLeft();
        print("dodgeleft");
        punchScript.Dodged = true;
        punchScript.DodgedSide = 0;
        punchScript.HPScriptTutorial.CurrentStamina -= punchScript.StaminaDodge;
    }
    public void NotDodging()
    {
        punchScript.Dodged = false;
        punchScript.DodgedSide = 2;
    }
}
