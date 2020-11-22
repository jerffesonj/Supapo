using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchIA : MonoBehaviour
{
    public PunchScript thisPunchScript;
    public PunchScript enemyPunchScript;
    public HPScript thisHpScript;
    public HPScript enemyHpScript;
    public DodgeScript dodgeScript;
    public GameObject exclamation;
    public Transform exclamationSpawn;

    public float timeToAttack;

    public float timeToChangeDifficulty = 0;
    public int randomDifficulty = 0;
    public bool mediumSelected = true;


    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    public Difficulty actualDifficulty;

    public bool once = true;

    public bool terminouesquiva = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameplayController.instance.actualStatus == GameplayController.Status.OnRound1 || GameplayController.instance.actualStatus == GameplayController.Status.OnRound2 || GameplayController.instance.actualStatus == GameplayController.Status.OnFinalRound)
        {
            switch (actualDifficulty)
            {
                case Difficulty.Easy:
                    Easy();

                    break;
                case Difficulty.Medium:
                    Medium();

                    break;

                case Difficulty.Hard:
                    Hard();

                    break;
            } 
        }
        else
        {
            thisPunchScript.block = false;
            thisPunchScript.dodged = false;
            thisPunchScript.atacou = false;
            thisPunchScript.hit = false;
            thisPunchScript.stunned = false;
        }
    }

    IEnumerator WaitForJab()
    {
        print("!!!!!!!");
        GameObject cloneExclamation = Instantiate(exclamation, exclamationSpawn);
        Destroy(cloneExclamation, 0.7f);
        yield return new WaitForSeconds(0.5f);
        thisPunchScript.Jab();
    }
    IEnumerator WaitForCross()
    {
        print("!!!!!!!");
        GameObject cloneExclamation = Instantiate(exclamation, exclamationSpawn);
        Destroy(cloneExclamation, 0.7f);
        yield return new WaitForSeconds(0.5f);
        thisPunchScript.Cross();
    }
    IEnumerator WaitForHook()
    {
        print("!!!!!!!");
        GameObject cloneExclamation = Instantiate(exclamation, exclamationSpawn);
        Destroy(cloneExclamation, 0.7f);
        yield return new WaitForSeconds(0.5f);
        thisPunchScript.HookLeft();
    }
    IEnumerator WaitForUpper()
    {
        print("!!!!!!!");
        GameObject cloneExclamation = Instantiate(exclamation, exclamationSpawn);
        Destroy(cloneExclamation, 0.7f);
        yield return new WaitForSeconds(0.5f);
        thisPunchScript.Uppercut();
    }

    IEnumerator WaitForBlock()
    {
        thisPunchScript.Block();
        yield return new WaitForSeconds(1.3f);
        thisPunchScript.block = false;
    }

    IEnumerator WaitForDodgeLeft()
    {
        dodgeScript.DodgedLeft();
        yield return new WaitForSeconds(1.3f);
        terminouesquiva = true;
        
    }
    IEnumerator WaitForDodgeRight()
    {
        dodgeScript.DodgedRight();
        yield return new WaitForSeconds(1.3f);
        terminouesquiva = true;
    }


    void Easy()
    {
        timeToAttack += Time.deltaTime;

        float randomTimetoAttack = Random.Range(2, 5);

        if (timeToAttack >= randomTimetoAttack)
        {
            if (thisPunchScript.block)
            {
                thisPunchScript.block = false;
            }
            if (!thisPunchScript.hit)
            {
                if (!thisPunchScript.stunned)
                {
                    if (!thisPunchScript.atacou)
                    {

                        int random = Mathf.RoundToInt((Random.Range(0, 8)));
                        //random = 5;
                        PunchIndex(random);

                        print(random);

                        timeToAttack = 0;

                    }
                }
            }
        }
    }

    void Medium()
    {
        timeToChangeDifficulty += Time.deltaTime;

        if (mediumSelected)
        {
            randomDifficulty = Random.Range(0, 2);

        }

        if (timeToChangeDifficulty >= 1f)
        {
            switch (randomDifficulty)
            {
                case 0:
                    Easy();
                    mediumSelected = false;
                    break;
                case 1:
                    Hard();
                    mediumSelected = false;
                    break;
            }
        }

        if (timeToChangeDifficulty >= 5)
        {
            timeToChangeDifficulty = 0;
            mediumSelected = true;
        }
    }

    void Hard()
    {
        timeToAttack += Time.deltaTime;

        int punchIndex = 0;

        if (thisHpScript.currentHp >= thisHpScript.maxHP / 3)
        {
            punchIndex = Random.Range(0, 2);
        }
        else
        {
            punchIndex = Random.Range(0, 4);

        }

        if (enemyPunchScript.stunned)
        {
            if (!thisPunchScript.hit)
            {
                if (!thisPunchScript.block)
                {
                    if (!thisPunchScript.atacou)
                    {
                        if (timeToAttack >= 1.3f)
                        {
                            once = true;
                            if (once)
                            {
                                PunchIndex(punchIndex);
                            }
                        }
                    }
                }
            }
        }
        else if (!enemyPunchScript.atacou)
        {
            if (!thisPunchScript.stunned)
            {
                if (!thisPunchScript.hit)
                {
                    if (!thisPunchScript.block)
                    {
                        if (!thisPunchScript.atacou)
                        {
                            if (timeToAttack >= 1.3f)
                            {
                                once = true;
                                if (once)
                                {
                                    PunchIndex(punchIndex);
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (!thisPunchScript.stunned)
            {
                if (!thisPunchScript.dodged)
                {
                    if (!thisPunchScript.hit)
                    {
                        if (!thisPunchScript.block)
                        {
                            if (thisHpScript.currentStun <= thisHpScript.maxStun / 2)
                            {
                                once = true;
                                if (once)
                                {
                                    punchIndex = 4;
                                    PunchIndex(punchIndex);
                                    print("defesa");
                                }
                            }
                            else
                            {
                                once = true;
                                if (once)
                                {
                                    punchIndex = Random.Range(5, 7);
                                    //punchIndex = 5;
                                    PunchIndex(punchIndex);
                                    print("esquiva");
                                    
                                }
                            }
                        }
                    }
                }
            }
        }
    }


    void PunchIndex(int punchIndex)
    {
        float randomTimeBlock = 0;
        float randomTimeDodge = 0;
        float randomTime = 0;
        switch (punchIndex)
        {
            case 0:

                StartCoroutine(WaitForJab());
                timeToAttack = 0;
                once = false;
                break;

            case 1:
                StartCoroutine(WaitForCross());
                timeToAttack = 0;
                once = false;
                break;

            case 2:
                StartCoroutine(WaitForHook());
                timeToAttack = 0;
                once = false;
                break;

            case 3:
                StartCoroutine(WaitForUpper());
                timeToAttack = 0;
                once = false;
                break;

            case 4:

                randomTime += Time.deltaTime;

                randomTimeBlock = Random.Range(0f, 0.2f);

                if (randomTime >= randomTimeBlock)
                {
                    StartCoroutine(WaitForBlock());

                }
                once = false;

                break;
            case 5:
                StartCoroutine(WaitForDodgeLeft());
                if (!terminouesquiva)
                {
                    once = true;
                }
                else
                {
                    once = false;
                }

                print("esquiva left");

                break;
            case 6:
                StartCoroutine(WaitForDodgeRight());

                if (!terminouesquiva)
                {
                    once = true;
                }
                else
                {
                    once = false;
                }
                print("esquiva right");
                break;
        }
    }
}