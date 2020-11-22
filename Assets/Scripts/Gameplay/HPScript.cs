using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPScript : MonoBehaviour
{
    [SerializeField] PunchScript thisPunchScript;
    [SerializeField] PunchScript otherPunchScript;
    [SerializeField] SpawnDamageCanvasFeedback spawnDamageCanvas;

    [SerializeField] GameObject perfectDefense;
    [SerializeField] Transform perfectDefenseSpawn;

    [SerializeField] GameObject perfectDodge;
    [SerializeField] Transform perfectDodgeSpawn;

    [SerializeField] GameObject combo;
    [SerializeField] Transform comboSpawn;

    [SerializeField] GameObject powerFx;
    [SerializeField] Transform powerFxTransfom;

    public float maxHP;
    public float currentHp;

    public float maxStamina;
    public float currentStamina;

    public float maxStun = 100;
    public float currentStun = 0;

    public float staminaRecharge;
    public float stunRecharge;

    public bool playOnce = true;


    // Start is called before the first frame update
    void Start()
    {
        //punchScript = GetComponent<PunchScript>();

        PunchScript otherPunchScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<PunchScript>();

        staminaRecharge = (thisPunchScript.thisAtributes.Resistance);
        staminaRecharge /= 200;
        staminaRecharge += 0.075f;

        stunRecharge = (thisPunchScript.thisAtributes.Resistance);
        stunRecharge /= 300;
        stunRecharge += 0.05f;

        maxHP = 100 + (thisPunchScript.thisAtributes.Resistance * 10);
        //currentHp = maxHP;

        maxStamina = 50 + (thisPunchScript.thisAtributes.Resistance * 10);
        //currentStamina = maxStamina;

        maxStun = 100 + (thisPunchScript.thisAtributes.Armor * 5);
        currentStun = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHp >= maxHP)
        {
            currentHp = maxHP;
        }

        if (currentStamina >= maxStamina)
        {
            currentStamina = maxStamina;
        }

        if (currentStun >= maxStun)
        {
            currentStun = maxStun;
        }

        if (currentStamina <= 0)
        {
            currentStamina = 0;
        }

        if (currentStun <= 0)
        {
            currentStun = 0;
        }

        if (GameplayController.instance.actualStatus == GameplayController.Status.OnRound1 || GameplayController.instance.actualStatus == GameplayController.Status.OnRound2 ||
                GameplayController.instance.actualStatus == GameplayController.Status.OnFinalRound)
        {
            if (!GameplayController.instance.pause)
            {
                RechargeHPStaminaStun();
            }

            if (currentHp <= 0)
            {
                currentHp = 0;
                if (this.CompareTag("Player"))
                {
                    GameplayController.instance.enemyRoundWonBool = true;
                    return;
                }
                if (this.CompareTag("Enemy"))
                {
                    GameplayController.instance.playerRoundWonBool = true;
                    return;
                }
            }

            Mathf.RoundToInt(currentStamina);
            Mathf.RoundToInt(currentStun);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.CompareTag("EnemyHand"))
            {
                CheckPunch();
            }
        }
        if (this.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.CompareTag("PlayerHand"))
            {
                CheckPunch();
            }
        }

    }

    public void RechargeHPStaminaStun()
    {
        if (thisPunchScript.block)
        {
            currentStamina += (staminaRecharge);

            if (!thisPunchScript.stunned)
            {
                currentStun -= stunRecharge;
            }
        }
        else
        {
            currentStamina += staminaRecharge;

            if (!thisPunchScript.stunned)
            {
                currentStun -= stunRecharge;
            }
        }
    }

    public void CheckPunch()
    {
        if ((GameplayController.instance.actualStatus == GameplayController.Status.OnRound1 ||
             GameplayController.instance.actualStatus == GameplayController.Status.OnRound2 ||
             GameplayController.instance.actualStatus == GameplayController.Status.OnFinalRound))
        {
            //damage
            float punchDamage = otherPunchScript.damage;
            float punchStun = otherPunchScript.punchStun;

            float reduction;
            reduction = thisPunchScript.thisAtributes.Armor;
            reduction /= 100;

            //ESQUIVANDO
            if (!thisPunchScript.dodged)
            {
                //DEFENDENDO
                if (thisPunchScript.block)
                {
                    if (thisPunchScript.tempoBlock >= 0.1 && thisPunchScript.tempoBlock <= 0.3)
                    {
                        print("defesa perfect");
                        currentStamina += punchStun * 5;
                        CheckHitPunchSidePerfectDefense(otherPunchScript.punchIndex);
                        Instantiate(perfectDefense, perfectDefenseSpawn);
                        StartCoroutine(DodgeBlockTimer());
                        //SoundGameplayController.instance.fxAudioSource2.pitch = 0.5f;
                        SoundGameplayController.instance.fxAudioSource2.PlayOneShot(SoundGameplayController.instance.perfectDefendSound);
                    }
                    else
                    {
                        if ((0.25f) * (punchDamage - reduction) <= currentHp)
                        {
                            currentHp -= (0.25f) * (punchDamage - reduction);
                        }
                        else
                        {
                            currentHp -= currentHp;
                        }
                        currentStamina -= (punchDamage * 0.75f);
                        currentStun += ((punchStun + otherPunchScript.thisAtributes.Strengh) / 2);
                        print("danoblock");

                        CheckHitPunchSideDefense(otherPunchScript.punchIndex);
                        SoundGameplayController.instance.fxAudioSource2.pitch = 0.7f;
                        SoundGameplayController.instance.fxAudioSource2.PlayOneShot(SoundGameplayController.instance.defendSound, 7f);
                    }
                }
                //NAO DEFENDENDO
                else
                {
                    //STUNADO
                    if (thisPunchScript.stunned)
                    {
                        if (this.gameObject.CompareTag("Player"))
                        {
                            spawnDamageCanvas.CameraShake(0.2f);
                            //GameObject hitCanvasClone = Instantiate(spawnDamageCanvas.hitCanvas, spawnDamageCanvas.canvasSpawn.transform);
                            GameplayController.instance.comboEnemy += 1;
                            GameplayController.instance.comboPlayer = 0;
                            GameObject comboTextClone = Instantiate(combo, comboSpawn.transform);
                            comboTextClone.GetComponentInChildren<TMP_Text>().text = "Combo \n" + "x " + GameplayController.instance.comboEnemy.ToString();


                        }
                        if (this.gameObject.CompareTag("Enemy"))
                        {
                            GameplayController.instance.comboPlayer += 1;
                            GameplayController.instance.comboEnemy = 0;
                            GameObject comboTextClone = Instantiate(combo, comboSpawn.transform);
                            comboTextClone.GetComponentInChildren<TMP_Text>().text = "Combo \n" + "x " + GameplayController.instance.comboPlayer.ToString();

                        }

                        if ((punchDamage - reduction) * 2 <= currentHp)
                        {
                            currentHp -= (punchDamage - reduction) * 2;
                        }
                        else
                        {
                            currentHp -= currentHp;
                        }

                        CheckSidePunchSoundStunned(otherPunchScript.punchIndex);

                    }
                    //NAOSTUNADO
                    else
                    {
                        if (otherPunchScript.atacou)
                        {
                            print(currentHp);
                            if (this.CompareTag("Player"))
                            {
                                SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.hitPlayer, 0.7f);
                            }
                            else if (this.CompareTag("Enemy"))
                            {
                                SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.hitEnemy, 0.7f);
                            }
                            thisPunchScript.Hit();
                            if ((punchDamage - reduction) <= currentHp)
                            {
                                currentHp -= (punchDamage - reduction);
                            }
                            else
                            {
                                currentHp -= currentHp;
                            }

                            currentStun += (punchStun + otherPunchScript.thisAtributes.Strengh);

                            print(currentHp);
                        }
                        else
                        {
                            if (this.CompareTag("Player"))
                            {
                                SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.hitPlayer, 0.7f);
                            }
                            else if (this.CompareTag("Enemy"))
                            {
                                SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.hitEnemy, 0.7f);
                            }

                            print(currentHp);
                            
                            thisPunchScript.Hit();
                            if ((punchDamage - reduction) * 1.5f <= currentHp)
                            {
                                currentHp -= (punchDamage - reduction) * 1.5f;
                            }
                            else
                            {
                                currentHp -= currentHp;
                            }

                            currentStun += (punchStun + otherPunchScript.thisAtributes.Strengh) * 1.5f;

                            print(currentHp);
                        }

                        if (this.gameObject.CompareTag("Enemy"))
                        {
                            GameplayController.instance.comboPlayer += 1;
                            GameplayController.instance.comboEnemy = 0;
                            GameObject comboTextClone = Instantiate(combo, comboSpawn.transform);
                            comboTextClone.GetComponentInChildren<TMP_Text>().text = "Combo \n" + "x " + GameplayController.instance.comboPlayer.ToString();
                        }
                        if (this.gameObject.CompareTag("Player"))
                        {
                            //GameObject hitCanvasClone = Instantiate(spawnDamageCanvas.hitCanvas, spawnDamageCanvas.canvasSpawn.transform);
                            GameplayController.instance.comboEnemy += 1;
                            GameplayController.instance.comboPlayer = 0;
                            GameObject comboTextClone = Instantiate(combo, comboSpawn.transform);
                            comboTextClone.GetComponentInChildren<TMP_Text>().text = "Combo \n" + "x " + GameplayController.instance.comboEnemy.ToString();
                            spawnDamageCanvas.CameraShake(0.1f);
                        }
                        print("levoudano");
                        CheckSidePunchSoundHit(otherPunchScript.punchIndex);
                    }
                }
            }
            //NÃO ESQUIVANDO
            else
            {
                if (thisPunchScript.tempoDodge <= 0.3 && thisPunchScript.tempoDodge <= 0.5)
                {
                    print("esquiva perfect");

                    //SoundGameplayController.instance.fxAudioSource2.pitch = 0.5f;
                    SoundGameplayController.instance.fxAudioSource2.PlayOneShot(SoundGameplayController.instance.dodgeSound);
                    
                    Instantiate(perfectDodge, perfectDodgeSpawn);
                    currentStun -= punchStun;
                    StartCoroutine(DodgeBlockTimer());
                    thisPunchScript.dodged = false;
                }
            }
        }

    }

    IEnumerator DodgeBlockTimer()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1;

    }

    public void CheckHitPunchSidePerfectDefense(float punchIndex)
    {
        if (punchIndex == 2 || punchIndex == 4)
        {
            spawnDamageCanvas.SpawnPowPerfectDefenseRightSide();
            print(otherPunchScript.punchIndex);
        }
        if (punchIndex == 1 || punchIndex == 3)
        {
            spawnDamageCanvas.SpawnPowPerfectDefenseLeftSide();
        }
    }

    void CheckHitPunchSideDefense(float punchIndex)
    {
        switch (punchIndex)
        {
            case 1:
                spawnDamageCanvas.SpawnPowDefenseLeftSide();
                print("playerasdas");
                spawnDamageCanvas.CameraShake(0.03f);
                break;

            case 2:
                spawnDamageCanvas.SpawnPowDefenseRightSide();
                print(otherPunchScript.punchIndex);
                print("playerasdas");
                spawnDamageCanvas.CameraShake(0.03f);
                break;

            case 3:
                spawnDamageCanvas.SpawnPowDefenseLeftSide();
                print("playerasdas");
                spawnDamageCanvas.CameraShake(0.03f);
                break;

            case 4:
                spawnDamageCanvas.SpawnPowDefenseRightSide();
                print(otherPunchScript.punchIndex);
                print("playerasdas");
                spawnDamageCanvas.CameraShake(0.03f);
                break;
        }
        
    }

    void CheckSidePunchSoundHit(float punchIndex)
    {
        switch (punchIndex)
        {
            case 1:
                spawnDamageCanvas.SpawnPowLeftSide();
                SoundGameplayController.instance.audioSource.PlayOneShot(SoundGameplayController.instance.jabSound);
                break;
            case 2:
                spawnDamageCanvas.SpawnPowRightSide();
                print(otherPunchScript.punchIndex);
                SoundGameplayController.instance.audioSource.PlayOneShot(SoundGameplayController.instance.diretoSound);
                break;
            case 3:
                spawnDamageCanvas.SpawnPowLeftSide();
                SoundGameplayController.instance.audioSource.PlayOneShot(SoundGameplayController.instance.hookSound);
                break;
            case 4:
                spawnDamageCanvas.SpawnPowRightSide();
                SoundGameplayController.instance.audioSource.PlayOneShot(SoundGameplayController.instance.upperSound);
                break;
        }
    }
    void CheckSidePunchSoundStunned(float punchIndex)
    {
        switch (punchIndex)
        {
            case 1:
                spawnDamageCanvas.SpawnPowStunLeftSide();
                SoundGameplayController.instance.audioSource.PlayOneShot(SoundGameplayController.instance.jabSound);
                break;
            case 2:
                spawnDamageCanvas.SpawnPowStunRightSide();
                print(otherPunchScript.punchIndex);
                SoundGameplayController.instance.audioSource.PlayOneShot(SoundGameplayController.instance.diretoSound);
                break;
            case 3:
                spawnDamageCanvas.SpawnPowStunLeftSide();
                SoundGameplayController.instance.audioSource.PlayOneShot(SoundGameplayController.instance.hookSound);
                break;
            case 4:
                spawnDamageCanvas.SpawnPowStunRightSide();
                SoundGameplayController.instance.audioSource.PlayOneShot(SoundGameplayController.instance.upperSound);
                break;
        }
    }

    public void TooFast4U()
    {

        if (!otherPunchScript.stunned)
        {
            thisPunchScript.animSpeed = thisPunchScript.animSpeedDefault;
            playOnce = true;
        }

        else
        {
            if (playOnce)
            {
                
                StartCoroutine(SlowTime());
                playOnce = false;
            }
        }
    }

    public IEnumerator SlowTime()
    {
        thisPunchScript.animSpeed *= 1.2f;

        SoundGameplayController.instance.audioSource.PlayOneShot(SoundGameplayController.instance.explosionStun, 2.5f);

        GameObject powerFxClone = Instantiate(powerFx, powerFxTransfom);

        yield return new WaitForSeconds(otherPunchScript.maxTimeStunado/2);

        Destroy(powerFxClone);
    }
}
