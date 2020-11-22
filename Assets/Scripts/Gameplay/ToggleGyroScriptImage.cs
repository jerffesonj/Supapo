using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGyroScriptImage : MonoBehaviour
{
    public Image checkImage;

    public Sprite checkOnImage;
    public Sprite checkOffImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuController.instance.UseGyro)
        {
            checkImage.sprite = checkOnImage;
        }
        else
        {
            checkImage.sprite = checkOffImage;
        }


    }
}
