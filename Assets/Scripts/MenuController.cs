using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    public bool onMenu = false;
    public bool useGyro = true;
    bool tutorial;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            Input.gyro.enabled = true;
            int tutorialIndex = PlayerPrefs.GetInt("Tutorial");
            if (tutorialIndex == 0)
            {
                tutorial = false;
            }
            else
            {
                tutorial = true;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Gets - Sets
    public bool OnMenu { get { return onMenu; } set { onMenu = value; } }
    public bool UseGyro { get { return useGyro; } set { useGyro = value; } }
    public bool Tutorial { get { return tutorial; } set { tutorial = value; } }
}