using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLoadScreenScene : MonoBehaviour
{

    public static ChangeLoadScreenScene instance;

    string sceneName;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSceneName(string sceneName)
    {
        this.sceneName = sceneName;
    }
    public string GetSceneName()
    {
        return sceneName;
    }
}
