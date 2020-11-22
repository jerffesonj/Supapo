using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOV : MonoBehaviour
{
    float x;
    float y;
    float aspect;
    float currentFov;

    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = this.GetComponent<Camera>();

        x = Screen.currentResolution.width;
        y = Screen.currentResolution.height;

        aspect = y / x;

        if (aspect >= 2)
        {
            currentFov = 77;
            camera.fieldOfView = currentFov;

        }
        else if (aspect > 1.77 && aspect < 2)
        {
            currentFov = 67;
            camera.fieldOfView = currentFov;
        }
     
    }

    public float CurrentFov { get { return currentFov; } }
}
