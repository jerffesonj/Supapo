using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golpe : MonoBehaviour
{

    public GameObject trailPrefab;
    public PunchScript punchScript;
    public DodgeScript dodgeScript;
    public float z;
    public float minCutVelocity;
    public float minTimeBlock;

    public float width;

    public bool touched;
    public Vector2 actualPosition;


    GameObject currentBladeTrail;
    Camera cam;
    public Rigidbody2D rb;
    public bool startCounting;
    public bool isCutting = false;
    public float timeTouch;
    public float velocity;
    public Vector2 touchStart;
    public Vector2 touchEnd;
    Vector2 previousPosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        print(cam);
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


    void CountAndCutBool(bool value)
    {
        isCutting = value;
        startCounting = value;
    }

    void StartTouchingMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = z;
        Vector2 newPosition = cam.ScreenToWorldPoint(mousePos);
        
        rb.position = newPosition;

        touchStart = rb.position;

        CountAndCutBool(true);

        currentBladeTrail = Instantiate(trailPrefab, transform);
    }

    void StopTouchingMouse()
    {
        CountAndCutBool(false);

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
                punchScript.atacou = true;
                punchScript.Jab();
                print("jab");
            }
            else if (timeTouch > 0.02f && timeTouch < minTimeBlock && rb.position.x > -3f)
            {
                punchScript.atacou = true;
                punchScript.Cross();
                print("direto");
            }
            else if ((touchEnd.x != 0) && touchEnd.y != 0)
            {
                if (Mathf.Abs(touchStart.x - touchEnd.x) > Mathf.Abs((touchStart.y - touchEnd.y) / 2))
                {
                    if (touchStart.x > touchEnd.x)
                    {
                        if (!punchScript.atacou)
                        {
                            punchScript.atacou = true;
                            punchScript.HookRight();
                            print("esquerda");
                        }
                    }
                    else if (touchStart.x < touchEnd.x)
                    {
                        if (!punchScript.atacou)
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
                        if (!punchScript.atacou)
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
                                    if (!punchScript.atacou)
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
                                    if (!punchScript.atacou)
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

        if (currentBladeTrail != null)
        {
            currentBladeTrail.transform.SetParent(null);
            Destroy(currentBladeTrail, 0.5f);
        }

    } 




    void UpdateCutMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = z;
        Vector2 newPosition = cam.ScreenToWorldPoint(mousePos);
        rb.position = newPosition;

        velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;

        previousPosition = newPosition;

        actualPosition = newPosition;

        if (timeTouch >= minTimeBlock)
        {
            if (!punchScript.atacou)
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