using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fps : MonoBehaviour
{
    TMP_Text fps;
    // Start is called before the first frame update
    void Start()
    {
        fps = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime != 0) 
            fps.text = Mathf.RoundToInt((1f / Time.smoothDeltaTime)).ToString();
    }
}
