using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTutorialScript : MonoBehaviour
{
    public GameObject trailPrefab;
    public PunchScriptTutorial punchScript;
    public DodgeScriptTutorial dodgeScript;
    public float z;
    public float minCutVelocity;
    public float minTimeBlock;

    public float width;

    public bool touched;
    public Vector2 actualPosition;


    GameObject currentBladeTrail;
    public Camera cam;
    public Rigidbody2D rb;
    public bool startCounting;
    public bool isCutting = false;
    public float timeTouch;
    public float velocity;
    public Vector2 touchStart;
    public Vector2 touchEnd;
    Vector2 previousPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if (TutorialManager.instance.actualState == TutorialManager.States.Pt12)
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
        //}
        //else
        //{
        //    punchScript.block = false;
        }


        //if (Application.isEditor)

        if ((TutorialManager.instance.actualState == TutorialManager.States.Pt3) ||
            (TutorialManager.instance.actualState == TutorialManager.States.Pt5) ||
            (TutorialManager.instance.actualState == TutorialManager.States.Pt7) ||
            (TutorialManager.instance.actualState == TutorialManager.States.Pt9) ||
            (TutorialManager.instance.actualState == TutorialManager.States.Pt12) ||
            (TutorialManager.instance.actualState == TutorialManager.States.Pt16)||
            (TutorialManager.instance.actualState == TutorialManager.States.Pt18))
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
    }

    void CountAndCutBool(bool value)
    {
        isCutting = value;
        startCounting = value;
    }

    void StartTouchingMouse()
    {
        if (!punchScript.Dodged)
        {
            if (!punchScript.Stunned)
            {
                cam.gameObject.SetActive(true);
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = z;
                Vector2 newPosition = cam.ScreenToWorldPoint(mousePos);
                rb.position = newPosition;

                touchStart = rb.position;

                CountAndCutBool(true);

                currentBladeTrail = Instantiate(trailPrefab, transform);
            }
        }
    }

    void StopTouchingMouse()
    {
        CountAndCutBool(false);

        if (!punchScript.Dodged)
        {
            //if (!punchScript.atacou)
            {
                if (punchScript.Block)
                {

                    punchScript.Block = false;
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
                        print("entrou tempo");
                        if (TutorialManager.instance.actualState == TutorialManager.States.Pt3 || (TutorialManager.instance.actualState == TutorialManager.States.Pt18))
                        {
                            if (punchScript.PunchIndex != 1)
                            {
                                punchScript.Atacou = true;
                                punchScript.Jab();
                                print("jab");
                            }
                        }
                    }
                    else if (timeTouch > 0.02f && timeTouch < minTimeBlock && rb.position.x > -3f)
                    {
                        if (TutorialManager.instance.actualState == TutorialManager.States.Pt5 || (TutorialManager.instance.actualState == TutorialManager.States.Pt18))
                        {
                            if (punchScript.PunchIndex != 1 && punchScript.PunchIndex != 2)
                            {
                                punchScript.Atacou = true;
                                punchScript.Cross();
                                print("direto");
                            }
                        }
                    }
                    else if ((touchEnd.x != 0) && touchEnd.y != 0)
                    {
                        if (Mathf.Abs(touchStart.x - touchEnd.x) > Mathf.Abs((touchStart.y - touchEnd.y) / 2))
                        {
                            if (TutorialManager.instance.actualState == TutorialManager.States.Pt7 || (TutorialManager.instance.actualState == TutorialManager.States.Pt18))
                            {
                                //if (touchStart.x > touchEnd.x)
                                //{
                                //    if (!punchScript.atacou)
                                //    {
                                //        punchScript.atacou = true;
                                //        punchScript.HookRight();
                                //        print("esquerda");
                                //    }
                                //}
                                //else 
                                if (touchStart.x < touchEnd.x)
                                {
                                    if (punchScript.PunchIndex != 1 && punchScript.PunchIndex != 2 && punchScript.PunchIndex != 3)
                                    {
                                        punchScript.Atacou = true;
                                        punchScript.HookLeft();
                                        print("direita");
                                    }
                                }
                            }
                        }
                        else if (Mathf.Abs(touchStart.x - touchEnd.x) < Mathf.Abs((touchStart.y - touchEnd.y) / 2))
                        {
                            if (touchStart.y < touchEnd.y)
                            {
                                if (TutorialManager.instance.actualState == TutorialManager.States.Pt9 || (TutorialManager.instance.actualState == TutorialManager.States.Pt18))
                                {
                                    if (punchScript.PunchIndex != 1 && punchScript.PunchIndex != 2 && punchScript.PunchIndex != 3 && punchScript.PunchIndex != 4)
                                    {
                                        punchScript.Atacou = true;
                                        punchScript.Uppercut();
                                        print("cima");
                                    }
                                }
                            }
                            else if (touchStart.y > touchEnd.y)
                            {
                                print("baixo");
                                if (TutorialManager.instance.actualState == TutorialManager.States.Pt16 || (TutorialManager.instance.actualState == TutorialManager.States.Pt18))
                                {
                                    if (rb.position.x < -3f)
                                    {
                                        //if (!MenuController.instance.useGyro)
                                        {
                                            if (!punchScript.Hit)
                                            {
                                                if (!punchScript.Atacou)
                                                {
                                                    if (!punchScript.Block)
                                                    {
                                                        if (!punchScript.Dodged)
                                                        {
                                                            if (punchScript.HPScriptTutorial.CurrentStamina >= punchScript.StaminaDodge)
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
                                        //if (!MenuController.instance.useGyro)
                                        {
                                            if (!punchScript.Hit)
                                            {
                                                if (!punchScript.Atacou)
                                                {
                                                    if (!punchScript.Block)
                                                    {
                                                        if (!punchScript.Dodged)
                                                        {
                                                            if (punchScript.HPScriptTutorial.CurrentStamina >= punchScript.StaminaDodge)
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
            }
        }
        if (currentBladeTrail != null)
        {
            currentBladeTrail.transform.SetParent(null);
            Destroy(currentBladeTrail, 0.5f);
        }
        touchEnd = new Vector2(0, 0);
        
    }

    void UpdateCutMouse()
    {

        {
            if (!punchScript.Block)
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
                    if (!punchScript.Atacou)
                    {
                        if (TutorialManager.instance.actualState == TutorialManager.States.Pt12 || TutorialManager.instance.actualState == TutorialManager.States.Pt18)
                            {
                            punchScript.GotBlock();
                            //print("block");
                            CountAndCutBool(false);

                        }
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
}