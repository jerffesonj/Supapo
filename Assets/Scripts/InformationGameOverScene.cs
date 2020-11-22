using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InformationGameOverScene : MonoBehaviour
{
    public static InformationGameOverScene instance;

    int comboMax;
    int timeLeft;
    int enemyLocationNumber = 1;
    int champWon = 0;
    int enemyAtributesValue;
    int comboValue;
    int timeValue;
    int hpValue;
    int fightValue;
    int totalValue;

    float hpLeft;

    bool playerWin;

    

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;


        }



    }

    // Update is called once per frame
    void Update()
    {
        if (InformationGameOverScene.instance.playerWin)
        {
            fightValue = ((enemyLocationNumber + enemyAtributesValue) * ((1 + champWon) * 10));
            comboValue = comboMax * 5;
            hpValue = (Mathf.RoundToInt(hpLeft * 2));
            timeValue = (timeLeft * 2);
            totalValue = comboValue + timeValue + hpValue + fightValue;
        }
        else
        {
            fightValue = Mathf.RoundToInt(((enemyLocationNumber + enemyAtributesValue) * ((1 + champWon) * 10)) / 2);
            comboValue = Mathf.RoundToInt((comboMax * 5) / 2);
            hpValue = 0;
            timeValue = 0;
            totalValue = comboValue + timeValue + hpValue + fightValue;

        }
    }

    public void ResetVariables()
    {
        comboMax = 0;
        hpLeft = 0;
        timeLeft = 0;
        fightValue = 0;
        comboValue = 0;
        hpValue = 0;
        timeValue = 0;
        totalValue = 0;
    }

    public int ComboMax { get { return comboMax; } set { comboMax = value; } }
    public int TimeLeft { get { return timeLeft; } set { timeLeft = value; } }
    public float HpLeft { get { return hpLeft; } set { hpLeft = value; } }
    public int EnemyLocation { get { return enemyLocationNumber; } set { enemyLocationNumber = value; } }
    public int ChampWon { get { return champWon; } set { champWon = value; } }
    public int EnemyAtributesValue { get { return enemyAtributesValue; } set { enemyAtributesValue = value; } }
    public int ComboValue { get { return comboValue; } set { comboValue = value; } }
    public int TimeValue { get { return timeValue; } set { timeValue = value; } }
    public int HpValue { get { return hpValue; } set { hpValue = value; } }
    public int FightValue { get { return fightValue; } set { fightValue = value; } }
    public int TotalValue { get { return totalValue; } set { totalValue = value; } }
    public bool PlayerWin { get { return playerWin; } set { playerWin = value; } }
}
