using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeScript : MonoBehaviour
{
    [Header ("Scripts")]
    [SerializeField] PunchScript punchScript;
    [SerializeField] DodgeScript dodgeScript;
    [Header("Camera")]
    [SerializeField] Camera cam;
    [Header("Game Objects")]
    [SerializeField] GameObject trailPrefab;
    [SerializeField] GameObject camObject;
    [Header("Transform")]
    [SerializeField] Transform camParent;
    [Header("Float")]
    [SerializeField] float z;
    [SerializeField] float minCutVelocity;
    [SerializeField] float minTimeBlock;

    bool startCounting;
    bool isCutting = false;

    float timeTouch;
    float velocity;

    Rigidbody2D rb;

    GameObject currentBladeTrail;

    Vector2 actualPosition;
    Vector2 touchStart;
    Vector2 touchEnd;
    Vector2 previousPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (!GameplayController.instance.pause)
        {
            if (GameplayController.instance.pausetimer > 0.5f)
            {
                if (startCounting)
                {
                    timeTouch += Time.deltaTime;
                    if (timeTouch >= 2)
                    {
                        timeTouch = 2;
                    }
                }
                else
                {
                    timeTouch = 0;
                }
                if (GameplayController.instance.actualStatus == GameplayController.Status.OnRound1 ||
                    GameplayController.instance.actualStatus == GameplayController.Status.OnRound2 ||
                    GameplayController.instance.actualStatus == GameplayController.Status.OnFinalRound)
                {

                    if (isCutting)
                    {
                        UpdateCutMouse();
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartTouchingMouse();
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        StopTouchingMouse();

                    }
                }
                else
                {
                    punchScript.block = false;
                    punchScript.dodged = false;
                    punchScript.atacou = false;
                    punchScript.hit = false;
                    punchScript.stunned = false;
                }
            }
            else
            {
                startCounting = false;
                isCutting = false;
            }
        }
    }

    void CountAndCutBool(bool value)
    {
        isCutting = value;
        startCounting = value;
    }

    void StartTouchingMouse()
    {
        if (!GameplayController.instance.pause)
        {
            if (!punchScript.dodged)
            {
                if (!punchScript.stunned)
                {
                    cam.gameObject.SetActive(true);
                    Vector3 mousePos = Input.mousePosition;
                    mousePos.z = z;
                    //camtranform = camObject.transform.position;

                    //camObject.transform.parent = camParent.parent;
                    //camObject.transform.position = campadraoTransform;

                    Vector2 newPosition = cam.ScreenToWorldPoint(mousePos);

                    //camObject.transform.parent = camParent;
                    //camObject.transform.position = camtranform;
                    rb.position = newPosition;

                    touchStart = rb.position;

                    CountAndCutBool(true);

                    currentBladeTrail = Instantiate(trailPrefab, transform);
                }
            }
        }
    }

    void StopTouchingMouse()
    {
        if (!GameplayController.instance.pause)
        {
            CountAndCutBool(false);

            if (!punchScript.dodged)
            {
                //if (!punchScript.atacou)
                {
                    if (punchScript.block)
                    {
                        punchScript.block = false;
                        print("semblock");

                        return;
                    }
                    else
                    {
                        if (velocity > minCutVelocity)
                        {
                            touchEnd = rb.position;
                        }
                        if (timeTouch > 0.02f && timeTouch < minTimeBlock && rb.position.x < -3f)
                        {
                            if (punchScript.punchIndex != 1)
                            {
                                punchScript.atacou = true;
                                punchScript.Jab();
                                print("jab");
                            }
                        }
                        else if (timeTouch > 0.02f && timeTouch < minTimeBlock && rb.position.x > -3f)
                        {
                            if (punchScript.punchIndex != 1 && punchScript.punchIndex != 2)
                            {
                                punchScript.atacou = true;
                                punchScript.Cross();
                                print("direto");
                            }
                        }
                        else if ((touchEnd.x != 0) && touchEnd.y != 0)
                        {
                            if (Mathf.Abs(touchStart.x - touchEnd.x) > Mathf.Abs((touchStart.y - touchEnd.y) / 2))
                            {
                                if (touchStart.x > touchEnd.x)
                                {
                                    if (punchScript.punchIndex != 1 && punchScript.punchIndex != 2 && punchScript.punchIndex != 3)
                                    {
                                        punchScript.atacou = true;
                                        punchScript.HookRight();
                                        print("esquerda");
                                    }
                                }
                                else if (touchStart.x < touchEnd.x)
                                {
                                    if (punchScript.punchIndex != 1 && punchScript.punchIndex != 2 && punchScript.punchIndex != 3)
                                    {
                                        punchScript.atacou = true;
                                        punchScript.HookLeft();
                                        print("direita");
                                    }
                                }
                            }
                            else if (Mathf.Abs(touchStart.x - touchEnd.x) < Mathf.Abs((touchStart.y - touchEnd.y) / 2))
                            {
                                if (touchStart.y < touchEnd.y)
                                {
                                    if (punchScript.punchIndex != 1 && punchScript.punchIndex != 2 && punchScript.punchIndex != 3 && punchScript.punchIndex != 4)
                                    {
                                        punchScript.atacou = true;
                                        punchScript.Uppercut();
                                        print("cima");
                                    }
                                }
                                else if (touchStart.y > touchEnd.y)
                                {
                                    print("baixo");
                                    if (rb.position.x < -3f)
                                    {
                                        if (!MenuController.instance.UseGyro)
                                        {
                                            if (!punchScript.hit)
                                            {
                                                if (!punchScript.stunned)
                                                {
                                                    if (!punchScript.block)
                                                    {
                                                        if (!punchScript.dodged)
                                                        {
                                                            if (punchScript.thisHPScript.currentStamina >= punchScript.staminaDodge)
                                                            {
                                                                dodgeScript.DodgedLeft();
                                                                print("esquerdo");
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (rb.position.x > -3f)
                                    {
                                        if (!MenuController.instance.UseGyro)
                                        {
                                            if (!punchScript.hit)
                                            {
                                                if (!punchScript.stunned)
                                                {
                                                    if (!punchScript.block)
                                                    {
                                                        if (!punchScript.dodged)
                                                        {
                                                            if (punchScript.thisHPScript.currentStamina >= punchScript.staminaDodge)
                                                            {
                                                                dodgeScript.DodgedRight();
                                                                print("direito");

                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                DestroyTrail();

            }
        }
    }

    void UpdateCutMouse()
    {
        if (!GameplayController.instance.pause)
        {
            if (!punchScript.block)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = z;
                Vector2 newPosition = cam.ScreenToWorldPoint(mousePos);
                cam.gameObject.SetActive(false);
                rb.position = newPosition;

                velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;

                previousPosition = newPosition;

                actualPosition = newPosition;

                if (timeTouch >= minTimeBlock)
                {
                    //if (!punchScript.atacou)
                    {
                        punchScript.Block();
                        //print("block");
                        CountAndCutBool(false);

                    }
                }

                if (velocity > minCutVelocity)
                {
                    startCounting = false;

                }
                else
                {
                    startCounting = true;
                }

            }

        }
    }
    void DestroyTrail()
    {
        if (currentBladeTrail != null)
        {
            currentBladeTrail.transform.SetParent(null);
            Destroy(currentBladeTrail, 0.5f);
        }
    }
}
