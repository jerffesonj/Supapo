using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchScriptTutorial : MonoBehaviour
{
    [SerializeField] StaminaFlash staminaFlash;
    
    [Header("Animation Speed")]
    [SerializeField] float animSpeed = 1;
    

    

    [Header("Dodge Parameters")]
    [SerializeField] float timeDodge;
    [SerializeField] float timeBlock;
    [SerializeField] float staminaDodge;

    [Header("Punch Damage")]
    [SerializeField] float jabDamage;
    [SerializeField] float diretoDamage;
    [SerializeField] float ganchoDamage;
    [SerializeField] float upperDamage;

    [Header("Punch Stamina")]
    [SerializeField] float jabStamina;
    [SerializeField] float diretoStamina;
    [SerializeField] float ganchoStamina;
    [SerializeField] float upperStamina;

    [Header("Punch Stun")]
    [SerializeField] float jabStun;
    [SerializeField] float diretoStun;
    [SerializeField] float ganchoStun;
    [SerializeField] float upperStun;

    [SerializeField] float tempoStunado = 0;
    [SerializeField] float tempoStunadoMax = 3;

    private HpScriptTutorial thisHPScript;
    private float timeHitMultiplier;
    private bool atacou = true;
    private bool hit;
    private bool block;
    private bool stunned;
    private bool dodged = false;
    private int dodgedSide = 0; //0 left 1 right
    private int punchIndex = 0;
    private float punchStamina;
    private float punchStun;
    private float damage;

    void Start()
    {
        thisHPScript = GetComponent<HpScriptTutorial>();
        animSpeed = 1;
    }

    void Update()
    {
        if (Dodged)
        {
            timeDodge += Time.deltaTime;
        }
        else
        {
            timeDodge = 0;
        }
        if (Block)
        {
            timeBlock += Time.deltaTime;
        }
        else
        {
            timeBlock = 0;
        }
        GotStunned();
    }

    public void Jab()
    {

        damage = jabDamage;
        //timeHitMultiplier = 0.8f;
        punchStamina = jabStamina;
        punchStun = jabStun;


        if (!Stunned)
        {
            if (thisHPScript.CurrentStamina >= punchStamina)
            {
                PunchIndex = 1;
                thisHPScript.CurrentStamina -= punchStamina;
            }
            else
            {
                Atacou = false;
                staminaFlash.Flash();
            }
        }
    }

    public void Cross()
    {
        damage = diretoDamage;
        //timeHitMultiplier = 0.6f;
        punchStamina = diretoStamina;
        punchStun = diretoStun;
        if (!Stunned)
        {
            if (thisHPScript.CurrentStamina >= punchStamina)
            {
                print("testeSoco");
                PunchIndex = 2;
                thisHPScript.CurrentStamina -= punchStamina;
            }
            else
            {
                Atacou = false;
                staminaFlash.Flash();
            }
        }
    }

    public void HookLeft()
    {
        damage = ganchoDamage;
        //timeHitMultiplier = 0.5f;
        punchStamina = ganchoStamina;
        punchStun = ganchoStun;
        if (!Stunned)
        {
            if (thisHPScript.CurrentStamina >= punchStamina)
            {
                PunchIndex = 3;
                thisHPScript.CurrentStamina -= punchStamina;
            }
            else
            {
                Atacou = false;
                staminaFlash.Flash();
            }
        }
    }

    public void HookRight()
    {
        damage = ganchoDamage;
        //timeHitMultiplier = 0.5f;
        punchStamina = ganchoStamina;
        punchStun = ganchoStun;
        if (!Stunned)
        {
            if (thisHPScript.CurrentStamina >= punchStamina)
            {
                PunchIndex = 3;
                thisHPScript.CurrentStamina -= punchStamina;
            }
            else
            {
                Atacou = false;
                staminaFlash.Flash();
            }
        }
    }

    public void Uppercut()
    {
        damage = upperDamage;
        //timeHitMultiplier = 0.4f;
        punchStamina = upperStamina;
        punchStun = upperStun;
        if (!Stunned)
        {
            if (thisHPScript.CurrentStamina >= punchStamina)
            {
                PunchIndex = 4;
                thisHPScript.CurrentStamina -= punchStamina;
            }
            else
            {
                Atacou = false;
                //staminaFlash.Flash();
            }
        }
    }

    public void GotBlock()
    {
        Block = true;
    }

    public void GotStunned()
    {

        if (thisHPScript.CurrentStun >= thisHPScript.MaxStun)
        {
            Stunned = true;
        }

        if (Stunned)
        {
            tempoStunado += Time.deltaTime;
            if (tempoStunado >= tempoStunadoMax)
            {
                thisHPScript.CurrentStun = 0;
                Stunned = false;
                tempoStunado = 0;
            }

        }
        else
        {


        }

    }

    public void GotHit()
    {
        Hit = true;
    }

    public HpScriptTutorial HPScriptTutorial { get { return thisHPScript; } }
    public float AnimSpeed { get { return animSpeed; } set { animSpeed = value; } }
    public float TimeHitMultiplier { get { return timeHitMultiplier; } set { timeHitMultiplier = value; } }
    public float PunchStun { get { return punchStun; } }
    public float Damage { get { return damage; } }
    public float TimeDodge { get { return timeDodge; } }
    public float TimeBlock { get { return timeBlock; } }
    public float StaminaDodge { get { return staminaDodge; } }
    public bool Atacou { get { return atacou; } set { atacou = value; } }
    public bool Hit { get { return hit; } set { hit = value; } }
    public bool Block { get { return block; } set { block = value; } }
    public bool Stunned { get { return stunned; } set { stunned = value; } }
    public bool Dodged { get { return dodged; } set { dodged = value; } }
    public int DodgedSide { get { return dodgedSide; } set { dodgedSide = value; } }

    public int PunchIndex { get { return punchIndex; } set { punchIndex = value; } }
}
