using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadScreenScript : MonoBehaviour
{
    

    bool loadScene;

    [SerializeField] TMP_Text loadingText;

    // Start is called before the first frame update
    void Start()
    {
        float x = Screen.currentResolution.width;
        float y = Screen.currentResolution.height;

        float aspect = y / x;

        if (aspect >= 2)
        {
            Screen.SetResolution(720, 1560, true);

        }
        else if (aspect > 1.77 && aspect < 2)
        {
            Screen.SetResolution(720, 1280, true);
        }

        Application.targetFrameRate = 30;

    }

    // Update is called once per frame
    void Update()
    {
        if (loadScene == false)
        {
            // ...set the loadScene boolean to true to prevent loading a new scene more than once...
            loadScene = true;
            // ...change the instruction text to read "Loading..."
            loadingText.text = "Carregando";
            // ...and start a coroutine that will load the desired scene.
            
            StartCoroutine(LoadNewScene());
        }
        // If the new scene has started loading...
        if (loadScene == true)
        {
            // ...then pulse the transparency of the loading text to let the player know that the computer is still working.
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        }
    }
    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator LoadNewScene()
    {
        string sceneName;
        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(0.5f);
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        if (ChangeLoadScreenScene.instance != null)
        {
            sceneName = ChangeLoadScreenScene.instance.GetSceneName();
        }
        else
        {
            sceneName = "Menu";
        }

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.

        while (!async.isDone)
        {
            print(async.progress);
            yield return null;
            
        }
        
        
    }
}
