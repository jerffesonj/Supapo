using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchIATutorial : MonoBehaviour
{
    [SerializeField] PunchScriptTutorial punchScript;
    [SerializeField] HpScriptTutorial hPScript;
    [SerializeField] DodgeScriptTutorial dodgeScript;
    [SerializeField] GameObject exclamation;
    [SerializeField] Transform exclamationSpawn;

    [SerializeField] float timeToAttack;
    float randomTimetoAttack;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        EnemyAttack();
    }

    private void EnemyAttack()
    {
        if ((TutorialManager.instance.actualState == TutorialManager.States.Pt12) ||
                    (TutorialManager.instance.actualState == TutorialManager.States.Pt14) ||
                    (TutorialManager.instance.actualState == TutorialManager.States.Pt16) ||
                    (TutorialManager.instance.actualState == TutorialManager.States.Pt18))
        {
            timeToAttack += Time.deltaTime;

            randomTimetoAttack = Random.Range(2, 5);

            if (timeToAttack >= randomTimetoAttack)
            {
                if (punchScript.Block)
                {
                    punchScript.Block = false;
                }
                if (!punchScript.Hit)
                {
                    if (!punchScript.Stunned)
                    {
                        if (!punchScript.Atacou)
                        {
                            int randomMax = 0;
                            if ((TutorialManager.instance.actualState == TutorialManager.States.Pt18))
                            {
                                randomMax = 8;
                            }
                            else
                            {
                                randomMax = 5;
                            }
                            int random = Mathf.RoundToInt((Random.Range(0, randomMax)));
                            //random = 4;
                            switch (random)
                            {
                                case 0:

                                    break;

                                case 1:

                                    StartCoroutine(WaitForJab());
                                    print("atacado");
                                    break;

                                case 2:
                                    StartCoroutine(WaitForCross());
                                    break;

                                case 3:
                                    StartCoroutine(WaitForCross());
                                    break;

                                case 4:
                                    StartCoroutine(WaitForUpper());
                                    break;

                                case 5:

                                    punchScript.GotBlock();
                                    break;
                                case 6:
                                    dodgeScript.DodgedLeft();
                                    break;
                                case 7:
                                    dodgeScript.DodgedRight();
                                    break;

                            }
                            print(random);

                            timeToAttack = 0;

                        }
                    }
                }
            }
        }
        else
        {
            //punchScript.block = false;
            //punchScript.dodged = false;
            //punchScript.atacou = false;
            ////punchScript.hit = false;
            //punchScript.stunned = false;
            randomTimetoAttack = 0;
        }
    }

    IEnumerator WaitForJab()
    {
        print("!!!!!!!");
        GameObject cloneExclamation = Instantiate(exclamation, exclamationSpawn);
        Destroy(cloneExclamation, 0.7f);
        yield return new WaitForSeconds(0.5f);
        punchScript.Jab();
    }

    IEnumerator WaitForCross()
    {
        print("!!!!!!!");
        GameObject cloneExclamation = Instantiate(exclamation, exclamationSpawn);
        Destroy(cloneExclamation, 0.7f);
        yield return new WaitForSeconds(0.5f);
        punchScript.Cross();
    }
    IEnumerator WaitForHook()
    {
        print("!!!!!!!");
        GameObject cloneExclamation = Instantiate(exclamation, exclamationSpawn);
        Destroy(cloneExclamation, 0.7f);
        yield return new WaitForSeconds(0.5f);
        punchScript.HookLeft();
    }
    IEnumerator WaitForUpper()
    {
        print("!!!!!!!");
        GameObject cloneExclamation = Instantiate(exclamation, exclamationSpawn);
        Destroy(cloneExclamation, 0.7f);
        yield return new WaitForSeconds(0.5f);
        punchScript.Uppercut();
    }
}