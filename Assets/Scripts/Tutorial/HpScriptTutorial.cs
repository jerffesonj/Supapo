using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HpScriptTutorial : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] SpawnDamageCanvasTutorial spawnDamageCanvas;

    [Header("Defesa")]
    [SerializeField] GameObject perfectDefense;
    [SerializeField] Transform perfectDefenseSpawn;

    [Header("Dodge")]
    [SerializeField] GameObject perfectDodge;
    [SerializeField] Transform perfectDodgeSpawn;

    [Header("HP")]
    [SerializeField] float maxHp;
    [SerializeField] float currentHp;

    [Header("Stamina")]
    [SerializeField] float maxStamina;
    [SerializeField] float currentStamina;

    [Header("Stun")]
    [SerializeField] float maxStun = 100;
    [SerializeField] float currentStun = 0;

    [Header("Recharge Stun/Stamina")]
    [SerializeField] float staminaRecharge;
    [SerializeField] float stunRecharge;

    PunchScriptTutorial thisPunchScript;
    PunchScriptTutorial otherPunchScript;

    // Start is called before the first frame update
    void Start()
    {
        //punchScript = GetComponent<PunchScript>();
        if (this.gameObject.tag == "Player")
        {
            thisPunchScript = GetComponent<PunchScriptTutorial>();
            otherPunchScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<PunchScriptTutorial>();
        }
        else if (this.gameObject.tag == "Enemy")
        {
            thisPunchScript = GetComponent<PunchScriptTutorial>();
            otherPunchScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PunchScriptTutorial>();
        }

        currentHp = maxHp;

        currentStamina = maxStamina;

        currentStun = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RechargeHPStaminaStun();

        if (currentStamina >= maxStamina)
        {
            currentStamina = maxStamina;
        }

        if (currentStun >= maxStun)
        {
            currentStun = maxStun;
        }

        if (currentStamina <= 30)
        {
            //currentStamina = maxStamina;
            StartCoroutine(FillStamina());
        }

        if (currentStun <= 0)
        {
            currentStun = 0;
        }


        if (currentHp <= 0)
        {
            //currentHp = maxHP;
            StartCoroutine(FillHP());
            
        }

        Mathf.RoundToInt(currentStamina);
        Mathf.RoundToInt(currentStun);
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
        if (thisPunchScript.Block)
        {
            currentStamina += (staminaRecharge);

            if (!thisPunchScript.Stunned)
            {
                currentStun -= stunRecharge;
            }
        }
        else
        {
            currentStamina += staminaRecharge;

            if (!thisPunchScript.Stunned)
            {
                currentStun -= stunRecharge;
            }
        }
    }

    IEnumerator FillHP()
    {
        while (currentHp < maxHp)
        {
            currentHp += 1f;
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }
    IEnumerator FillStamina()
    {
        while (currentStamina < maxStamina)
        {
            currentStamina += 1f;
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }


    public void CheckPunch()
    {
        //damage
        float punchDamage = otherPunchScript.Damage;
        float punchStun = otherPunchScript.PunchStun;

        float reduction = 0;

        //ESQUIVANDO
        if (!thisPunchScript.Dodged)
        {
            //DEFENDENDO
            if (thisPunchScript.Block)
            {
                if (thisPunchScript.TimeBlock >= 0.1 && thisPunchScript.TimeBlock <= 0.3)
                {
                    print("defesa perfect");
                    currentStamina += punchStun * 5;
                    CheckHitPunchSidePerfectDefense(otherPunchScript.PunchIndex);
                    SoundGameplayController.instance.fxAudioSource2.PlayOneShot(SoundGameplayController.instance.perfectDefendSound);
                    Instantiate(perfectDefense, perfectDefenseSpawn);
                    StartCoroutine(DodgeBlockTimer());
                    //SoundGameplayController.instance.fxAudioSource2.pitch = 0.5f;
                    
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
                    currentStun += punchStun;
                    print("danoblock");

                    CheckHitPunchSideDefense(otherPunchScript.PunchIndex);
                    SoundGameplayController.instance.fxAudioSource2.pitch = 0.7f;
                    SoundGameplayController.instance.fxAudioSource2.PlayOneShot(SoundGameplayController.instance.defendSound, 7f);
                }
            }
            //NAO DEFENDENDO
            else
            {
                //STUNADO
                if (thisPunchScript.Stunned)
                {

                    if ((punchDamage - reduction) * 2 <= currentHp)
                    {
                        currentHp -= (punchDamage - reduction) * 2;
                    }
                    else
                    {
                        currentHp -= currentHp;
                    }

                    CheckSidePunchSoundStunned(otherPunchScript.PunchIndex);

                }
                //NAOSTUNADO
                else
                {
                    if (otherPunchScript.Atacou)
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
                        thisPunchScript.GotHit();
                        if ((punchDamage - reduction) <= currentHp)
                        {
                            currentHp -= (punchDamage - reduction);
                        }
                        else
                        {
                            currentHp -= currentHp;
                        }

                        currentStun += (punchStun);

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

                        thisPunchScript.GotHit();

                        if ((punchDamage - reduction) * 1.5f <= currentHp)
                        {
                            currentHp -= (punchDamage - reduction) * 1.5f;
                        }
                        else
                        {
                            currentHp -= currentHp;
                        }

                        currentStun += (punchStun) * 1.5f;

                        print(currentHp);
                    }

                    print("levoudano");
                    CheckSidePunchSoundHit(otherPunchScript.PunchIndex);
                }
            }
        }
        //NÃO ESQUIVANDO
        else
        {
            if (thisPunchScript.TimeDodge <= 0.3 && thisPunchScript.TimeDodge <= 0.5)
            {
                print("esquiva perfect");

                //SoundGameplayController.instance.fxAudioSource2.pitch = 0.5f;
                SoundGameplayController.instance.fxAudioSource2.PlayOneShot(SoundGameplayController.instance.dodgeSound);

                Instantiate(perfectDodge, perfectDodgeSpawn);
                currentStun -= punchStun;
                StartCoroutine(DodgeBlockTimer());
                thisPunchScript.Dodged = false;
            }
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
                print("playerasdas");
                spawnDamageCanvas.CameraShake(0.03f);
                break;
        }

    }



    ////damage
    //float punchDamage = otherPunchScript.Damage;
    //        float punchStun = otherPunchScript.PunchStun;

    //        float reduction = 0;

    //        if (!thisPunchScript.Dodged)
    //        {
    //            if (thisPunchScript.Block)
    //            {
    //                if (thisPunchScript.TimeBlock >= 0.1 && thisPunchScript.TimeBlock <= 0.3)
    //                {
    //                    print("defesa perfect");
    //                    currentStamina += punchStun * 5;
    //                    CheckHitPunchSidePerfectDefense(otherPunchScript.PunchIndex);
    //                    GameObject clonePerfectDefense = Instantiate(perfectDefense, perfectDefenseSpawn);
    //                    clonePerfectDefense.GetComponent<TMP_Text>().text = "Defesa Perfeita";
    //                    StartCoroutine(DodgeBlockTimer());
    //                }
    //                else
    //                {
    //                    currentHp -= (0.25f) * (punchDamage - reduction);
    //                    currentStamina -= (punchDamage * 0.75f);
    //                    currentStun += ((punchStun));
    //                    print("danoblock");

    //                    CheckHitPunchSideDefense(otherPunchScript.PunchIndex);
    //                }
    //            }
    //            else
    //            {
    //                if (thisPunchScript.Stunned)
    //                {
    //                    if (this.gameObject.CompareTag("Player"))
    //                    {
    //                        spawnDamageCanvas.CameraShake(0.2f);
    //                        if (otherPunchScript.PunchIndex == 2)
    //                        {
    //                            spawnDamageCanvas.SpawnPowStunRightSide();
    //                            print(otherPunchScript.PunchIndex);
    //                        }
    //                        if (otherPunchScript.PunchIndex == 1 || otherPunchScript.PunchIndex == 3 || otherPunchScript.PunchIndex == 4)
    //                        {
    //                            spawnDamageCanvas.SpawnPowStunLeftSide();
    //                        }
    //                        //GameObject hitCanvasClone = Instantiate(spawnDamageCanvas.hitCanvas, spawnDamageCanvas.canvasSpawn.transform);
    //                    }
    //                    if (this.gameObject.CompareTag("Enemy"))
    //                    {
    //                        if (otherPunchScript.PunchIndex == 2)
    //                        {
    //                            spawnDamageCanvas.SpawnPowStunRightSide();
    //                            print(otherPunchScript.PunchIndex);
    //                        }
    //                        if (otherPunchScript.PunchIndex == 1 || otherPunchScript.PunchIndex == 3 || otherPunchScript.PunchIndex == 4)
    //                        {
    //                            spawnDamageCanvas.SpawnPowStunLeftSide();
    //                        }
    //                    }
    //                    currentHp -= (punchDamage - reduction) * 2;
    //                }
    //                else
    //                {
    //                    if (otherPunchScript.Atacou)
    //                    {
    //                        print(currentHp);
    //                        currentStun += (punchStun);
    //                        currentHp -= (punchDamage - reduction);
    //                        print(currentHp);
    //                        thisPunchScript.GotHit();
    //                    }
    //                    else
    //                    {
    //                        print(currentHp);
    //                        currentStun += (punchStun);
    //                        currentHp -= (punchDamage - reduction) * 1.5f;
    //                        print(currentHp);
    //                        thisPunchScript.GotHit();
    //                    }

    //                    if (this.gameObject.CompareTag("Enemy"))
    //                    {
    //                        if (otherPunchScript.PunchIndex == 2)
    //                        {
    //                            spawnDamageCanvas.SpawnPowRightSide();
    //                            print(otherPunchScript.PunchIndex);
    //                        }
    //                        if (otherPunchScript.PunchIndex == 1 || otherPunchScript.PunchIndex == 3 || otherPunchScript.PunchIndex == 4)
    //                        {
    //                            spawnDamageCanvas.SpawnPowLeftSide();
    //                        }
    //                    }
    //                    if (this.gameObject.CompareTag("Player"))
    //                    {

    //                        //GameObject hitCanvasClone = Instantiate(spawnDamageCanvas.hitCanvas, spawnDamageCanvas.canvasSpawn.transform);
    //                        spawnDamageCanvas.CameraShake(0.1f);
    //                    }
    //                    print("levoudano");
    //                }
    //            }
    //        }
    //        else
    //        {
    //            if (thisPunchScript.TimeDodge <= 0.3 && thisPunchScript.TimeDodge <= 0.5)
    //            {
    //                print("esquiva perfect");
    //                GameObject clonePerfectDefense = Instantiate(perfectDodge, perfectDodgeSpawn);
    //                clonePerfectDefense.GetComponent<TMP_Text>().text = "Esquiva Perfeita";
    //                currentStun -= punchStun;
    //                StartCoroutine(DodgeBlockTimer());
    //                thisPunchScript.Dodged = false;
    //            }
    //    }
    //}

    IEnumerator DodgeBlockTimer()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1;

    }

    void CheckHitPunchSidePerfectDefense(float punchIndex)
    {
        if (punchIndex == 2)
        {
            spawnDamageCanvas.SpawnPowPerfectDefenseRightSide();
            print(otherPunchScript.PunchIndex);
        }
        if (punchIndex == 1 || punchIndex == 3 || punchIndex == 4)
        {
            spawnDamageCanvas.SpawnPowPerfectDefenseLeftSide();
        }
    }


    public float MaxHp { get { return maxHp; } set { maxHp = value; } }
    public float CurrentHp { get { return currentHp; } set { currentHp = value; } }
    public float MaxStamina { get { return maxStamina; } set { maxStamina = value; } }
    public float CurrentStamina { get { return currentStamina; } set { currentStamina = value; } }
    public float MaxStun { get { return maxStun; } set { maxStun = value; } }
    public float CurrentStun { get { return currentStun; } set { currentStun = value; } }
}
