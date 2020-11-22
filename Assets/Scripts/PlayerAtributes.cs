using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtributes : MonoBehaviour
{
    public static PlayerAtributes instance;

    [Header("Money")]
    [SerializeField] int totalMoney;

    [Header("XP")]
    [SerializeField] int maxXp;
    [SerializeField] int totalXp;

    [Header("Level")]
    [SerializeField] int playerLvl;
    [SerializeField] int atributesLvlUp;
    [SerializeField] int boughtAtributes;

    [Header("Atributes")]
    [SerializeField] int strengh;
    [SerializeField] int agility;
    [SerializeField] int armor;
    [SerializeField] int resistance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    void Update()
    {
        LevelUP();
    }

    void LevelUP()
    {
        if (totalXp >= maxXp)
        {
            playerLvl += 1;
            atributesLvlUp += 1;
            totalXp -= maxXp;
            maxXp += 100;
        }
    }

    //Gets - Sets
    public int TotalMoney { get { return totalMoney; } set { totalMoney = value; } }
    public int MaxXp { get { return maxXp; } set { maxXp = value; } }
    public int TotalXp { get { return totalXp; } set { totalXp = value; } }
    public int PlayerLvl { get { return playerLvl; } set { playerLvl = value; } }
    public int AtributesLvlUp { get { return atributesLvlUp; } set { atributesLvlUp = value; } }
    public int BoughtAtributes { get { return boughtAtributes; } set { boughtAtributes = value; } }
    public int Strengh { get { return strengh; } set { strengh = value; } }
    public int Agility { get { return agility; } set { agility = value; } }
    public int Armor { get { return armor; } set { armor = value; } }
    public int Resistance { get { return resistance; } set { resistance = value; } }
}
