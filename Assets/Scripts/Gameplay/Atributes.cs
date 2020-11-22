using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atributes : MonoBehaviour
{

    [SerializeField] int strengh;
    [SerializeField] int agility;
    [SerializeField] int armor;
    [SerializeField] int resistance;

    [SerializeField] int allPlayerAtributes = 0;

    [SerializeField] List<int> numbersAtributesList = new List<int>();

    private void Awake()
    {
        if (gameObject.CompareTag("Player"))
        {
            if (PlayerAtributes.instance != null)
            {
                strengh = PlayerAtributes.instance.Strengh;
                agility = PlayerAtributes.instance.Agility;
                armor = PlayerAtributes.instance.Armor;
                resistance = PlayerAtributes.instance.Resistance;
            }
        }

        if (gameObject.CompareTag("Enemy"))
        {
            if (PlayerAtributes.instance != null)
            {

                allPlayerAtributes = PlayerAtributes.instance.Strengh +
                                     PlayerAtributes.instance.Agility +
                                     PlayerAtributes.instance.Armor +
                                     PlayerAtributes.instance.Resistance + 3;

                InformationGameOverScene.instance.EnemyAtributesValue = allPlayerAtributes;

                for (int i = 0; i < 4; i++)
                {
                    if (allPlayerAtributes > 0)
                    {

                        int random;
                        random = Mathf.RoundToInt(Random.Range(0, allPlayerAtributes + 1));

                        numbersAtributesList.Add(random);

                        allPlayerAtributes -= random;
                    }
                    else
                    {
                        numbersAtributesList.Add(0);
                    }
                }

                int randomList;

                randomList = Mathf.RoundToInt(Random.Range(0, numbersAtributesList.Count));
                strengh = numbersAtributesList[randomList];
                print(strengh);
                numbersAtributesList.RemoveAt(randomList);

                randomList = Mathf.RoundToInt(Random.Range(0, numbersAtributesList.Count));
                agility = numbersAtributesList[randomList];
                print(agility);
                numbersAtributesList.RemoveAt(randomList);

                randomList = Mathf.RoundToInt(Random.Range(0, numbersAtributesList.Count));
                armor = numbersAtributesList[randomList];
                print(armor);
                numbersAtributesList.RemoveAt(randomList);

                randomList = Mathf.RoundToInt(Random.Range(0, numbersAtributesList.Count));
                resistance = numbersAtributesList[randomList];
                print(resistance);
                numbersAtributesList.RemoveAt(randomList);

            }
        }
    }

    public int Strengh { get { return strengh; } }
    public int Agility { get { return agility; } }
    public int Armor { get { return armor; } }
    public int Resistance { get { return resistance; } }

}

