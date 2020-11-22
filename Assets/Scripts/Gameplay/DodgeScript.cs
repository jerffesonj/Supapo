using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DodgeScript : MonoBehaviour
{
    [SerializeField] Transform local;

    float accelerationValue;
    float time;
    float actualValueAcel;
    float lastValueAcel;
    float timeChange;

    PunchScript punchScript;

    Vector3 acel;
    Vector3 acelVector;

    // Start is called before the first frame update
    void Start()
    {
        punchScript = GetComponent<PunchScript>();
        //Input.gyro.updateInterval = 0.0167f;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.CompareTag("Player"))
        {
            //accelerationValue = Input.acceleration.x;
            acel = Input.acceleration;
            acelVector = new Vector3(12f, 0, 0);
            Vector3 vectorZero = Vector3.zero;
            Vector3.SmoothDamp(acel, acelVector, ref vectorZero, 2);
            accelerationValue = acel.x;
        }

        if (GameplayController.instance.actualStatus == GameplayController.Status.OnRound1 || 
            GameplayController.instance.actualStatus == GameplayController.Status.OnRound2 || 
            GameplayController.instance.actualStatus == GameplayController.Status.OnFinalRound)
        {
            CheckDodgeSide();

            if (CompareTag("Player")) {
                if (MenuController.instance.UseGyro)
                {
                    if (accelerationValue >= 0.4f && accelerationValue <= 0.6f)
                    //if (Input.GetMouseButton(1))
                    {
                        if (!punchScript.hit)
                        { 
                            //if (!punchScript.atacou)
                            {
                                if (!punchScript.block)
                                {
                                    if (!punchScript.dodged)
                                    {
                                        if (!punchScript.stunned)
                                        {
                                            DodgedRight();
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (accelerationValue >= -0.6f && accelerationValue <= -0.4f)
                    {
                        if (!punchScript.hit)
                        {
                            //if (!punchScript.atacou)
                            {
                                if (!punchScript.block)
                                {
                                    if (!punchScript.dodged)
                                    {
                                        if (!punchScript.stunned)
                                        {
                                            DodgedLeft();
                                            return;
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
        }
    }

    public void DodgedRight()
    {
        if (punchScript.thisHPScript.currentStamina >= punchScript.staminaDodge)
        {
            //punchScript.DodgeRight();
            print("dodgeright");
            if (punchScript.atacou)
            {
                punchScript.atacou = false;
                punchScript.punchIndex = 0;
            }

            punchScript.dodged = true;
            punchScript.dodgedSide = 1;
            punchScript.thisHPScript.currentStamina -= punchScript.staminaDodge;
        }

    }
    public void DodgedLeft()
    {
        if (punchScript.thisHPScript.currentStamina >= punchScript.staminaDodge)
        {
            if (punchScript.atacou)
            {
                punchScript.atacou = false;
                punchScript.punchIndex = 0;
            }
            //punchScript.DodgeLeft();
            print("dodgeleft");
            punchScript.dodged = true;
            punchScript.dodgedSide = 0;
            punchScript.thisHPScript.currentStamina -= punchScript.staminaDodge;
        }
    }
    public void NotDodging()
    {
        punchScript.dodged = false;
        punchScript.dodgedSide = 2;
    }

    void CheckDodgeSide()
    {
        if (punchScript.dodged)
        {
            if (punchScript.dodgedSide == 0)
            {
                Vector3 rotationLocal;
                rotationLocal = local.rotation.eulerAngles;
                rotationLocal.y += 0.3f;
                local.rotation = Quaternion.Euler(rotationLocal);
            }
            if (punchScript.dodgedSide == 1)
            {
                Vector3 rotationLocal;
                rotationLocal = local.rotation.eulerAngles;
                rotationLocal.y -= 0.3f;
                local.rotation = Quaternion.Euler(rotationLocal);
            }
        }
    }

}
