using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Version : MonoBehaviour
{
    // Start is called before the first frame update
    TMP_Text version;
    void Start()
    {
        version = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        version.text = Application.version;
    }
}
