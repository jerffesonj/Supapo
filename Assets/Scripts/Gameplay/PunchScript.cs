using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchScript : MonoBehaviour
{
    public HPScript thisHPScript;
    public Atributes thisAtributes;

    public float animSpeed = 1;
    public float animSpeedDefault;
    public float timeHitMultiplier;

    public float punchStamina;
    public float punchStun;
    public float damage;

    public float tempoDodge;
    public float tempoBlock;

    [Header("Dano")]
    public float jabDamage;
    public float diretoDamage;
    public float ganchoDamage;
    public float upperDamage;

    [Header("Stamina")]
    public float jabStamina;
    public float diretoStamina;
    public float ganchoStamina;
    public float upperStamina;

    public float staminaDodge;

    [Header("Stun")]
    public float jabStun;
    public float diretoStun;
    public float ganchoStun;
    public float upperStun;

    float tempoStunado = 0;
    public float maxTimeStunado;

    public bool atacou = true;
    public bool hit;
    public bool block;
    public bool stunned;
    public bool dodged = false;
    public int dodgedSide = 0; //0 left 1 right
    public bool stayDown = false;


    public int punchIndex = 0;

    public StaminaFlash staminaFlash;

    void Start()
    {
        animSpeed = thisAtributes.Agility;
        animSpeed /= 100;
        animSpeed += 1;

        animSpeedDefault = animSpeed;
        //staminaDodge = thisHPScript.maxStamina/ 10;
    }

    void Update()
    {
        if (!GameplayController.instance.pause)
        {
            if (dodged)
            {
                tempoDodge += Time.deltaTime;
            }
            else
            {
                tempoDodge = 0;
            }
            if (block)
            {
                tempoBlock += Time.deltaTime;
            }
            else
            {
                tempoBlock = 0;
            }

            thisHPScript.TooFast4U();



            
            //StateName();
            Stunned();
            //ReturnToIdle();
        }
    }

    public void Jab()
    {

        damage = jabDamage + thisAtributes.Strengh;
        //timeHitMultiplier = 0.8f;
        punchStamina = jabStamina + (thisAtributes.Strengh/2);
        punchStun = jabStun;

        if (gameObject.CompareTag("Player"))
        {
            if (!stunned)
            {
                if (GameplayController.instance.PlayerHpScript.currentStamina >= punchStamina)
                {
                    punchIndex = 1;
                    GameplayController.instance.PlayerHpScript.currentStamina -= punchStamina;
                    SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.beginPunch, 1f);
                }
                else
                {
                    atacou = false;
                    staminaFlash.Flash();
                    SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.tired, 0.3f);
                }
            }
        }

        if (gameObject.CompareTag("Enemy"))
        {
            if (!stunned)
            {
                if (GameplayController.instance.EnemyHpScript.currentStamina >= punchStamina)
                {
                    punchIndex = 1;
                    GameplayController.instance.EnemyHpScript.currentStamina -= punchStamina;
                    SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.beginPunch, 1f);
                }
                else
                {
                    atacou = true;
                    staminaFlash.Flash();
                    SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.tired, 0.3f);
                }
            }
        }
    }

    public void Cross()
    {
        damage = diretoDamage + thisAtributes.Strengh;
        //timeHitMultiplier = 0.6f;
        punchStamina = diretoStamina + (thisAtributes.Strengh / 2);
        punchStun = diretoStun;
        if (gameObject.CompareTag("Player"))
        {
            if (!stunned)
            {
                if (GameplayController.instance.PlayerHpScript.currentStamina >= punchStamina)
                {
                    print("testeSoco");
                    punchIndex = 2;
                    GameplayController.instance.PlayerHpScript.currentStamina -= punchStamina;
                    SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.beginPunch, 1.15f);
                }
                else
                {
                    atacou = false;
                    staminaFlash.Flash();
                    SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.tired, 0.3f);
                }
            }
        }
        if (gameObject.CompareTag("Enemy"))
        {
            if (!stunned)
            {
                if (GameplayController.instance.EnemyHpScript.currentStamina >= punchStamina)
                {
                    punchIndex = 2;
                    GameplayController.instance.EnemyHpScript.currentStamina -= punchStamina;
                    SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.beginPunch, 1.15f);
                }
                else
                {
                    atacou = false;
                    staminaFlash.Flash();
                    SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.tired, 0.3f);
                }
            }
        }
    }

    public void HookLeft()
    {
        damage = ganchoDamage + thisAtributes.Strengh;
        //timeHitMultiplier = 0.5f;
        punchStamina = ganchoStamina + (thisAtributes.Strengh / 2);
        punchStun = ganchoStun;
        if (gameObject.CompareTag("Player"))
        {
            if (!stunned)
            {
                if (GameplayController.instance.PlayerHpScript.currentStamina >= punchStamina)
                {
                    punchIndex = 3;
                    GameplayController.instance.PlayerHpScript.currentStamina -= punchStamina;
                    SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.beginPunch, 1.3f);
                }
                else
                {
                    atacou = false;
                    staminaFlash.Flash();
                    SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.tired, 0.3f);
                }
            }
        }
        if (gameObject.CompareTag("Enemy"))
        {
            if (!stunned)
            {
                if (GameplayController.instance.EnemyHpScript.currentStamina >= punchStamina)
                {
                    punchIndex = 3;
                    GameplayController.instance.EnemyHpScript.currentStamina -= punchStamina;
                    SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.beginPunch, 1.3f);
                }
                else
                {
                    atacou = false;
                    staminaFlash.Flash();
                    SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.tired, 0.3f);
                }
            }
        }
    }
    public void HookRight()
    {
        damage = ganchoDamage + thisAtributes.Strengh;
        //timeHitMultiplier = 0.5f;
        punchStamina = ganchoStamina + (thisAtributes.Strengh / 2);
        punchStun = ganchoStun;
        if (gameObject.CompareTag("Player"))
        {
            if (!stunned)
            {
                if (GameplayController.instance.PlayerHpScript.currentStamina >= punchStamina)
                {
                    punchIndex = 3;
                    GameplayController.instance.PlayerHpScript.currentStamina -= punchStamina;
                    SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.beginPunch, 1.3f);

                }
                else
                {
                    atacou = false;
                    staminaFlash.Flash();
                    SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.tired, 0.3f);
                }
            }
        }
        if (gameObject.CompareTag("Enemy"))
        {
            if (!stunned)
            {
                if (GameplayController.instance.PlayerHpScript.currentStamina >= punchStamina)
                {
                    punchIndex = 3;
                    GameplayController.instance.PlayerHpScript.currentStamina -= punchStamina;
                    SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.beginPunch, 1.3f);
                }
                else
                {
                    atacou = false;
                    staminaFlash.Flash();
                    SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.tired, 0.3f);
                }
            }
        }
    }
    public void Uppercut()
    {
        damage = upperDamage + thisAtributes.Strengh;
        //timeHitMultiplier = 0.4f;
        punchStamina = upperStamina + (thisAtributes.Strengh / 2);
        punchStun = upperStun;
        if (gameObject.CompareTag("Player"))
        {
            if (!stunned)
            {
                if (GameplayController.instance.PlayerHpScript.currentStamina >= punchStamina)
                {
                    punchIndex = 4;
                    GameplayController.instance.PlayerHpScript.currentStamina -= punchStamina;
                    SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.beginPunch, 1.5f);
                }
                else
                {
                    atacou = false;
                    staminaFlash.Flash();
                    SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.tired, 0.3f);
                }
            }
        }
        if (gameObject.CompareTag("Enemy"))
        {
            if (!stunned)
            {
                if (GameplayController.instance.EnemyHpScript.currentStamina >= punchStamina)
                {
                    punchIndex = 4;
                    GameplayController.instance.EnemyHpScript.currentStamina -= punchStamina;
                    SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.beginPunch, 1.5f);
                }
                else
                {
                    atacou = false;
                    staminaFlash.Flash();
                    SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.tired, 0.3f);
                }
            }
        }
    }
    public void Block()
    {
        block = true;
    }

    public void Stunned()
    {
        if (thisHPScript.currentStun >= thisHPScript.maxStun)
        {
            stunned = true;
        }

        if (stunned)
        {
            tempoStunado += Time.deltaTime;
            if (tempoStunado >= maxTimeStunado)
            {
                thisHPScript.currentStun = 0;
                stunned = false;
                tempoStunado = 0;
            }
        }
        else
        {
        }
    }

    public void Hit()
    {
        hit = true;
    }
}