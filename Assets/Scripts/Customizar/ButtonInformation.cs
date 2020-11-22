using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInformation : MonoBehaviour
{
    [SerializeField] GameObject information;
   
    public void PointerDown()
    {
        SoundControllerCustom.instance.AudioSource.PlayOneShot(SoundControllerCustom.instance.SelectedFx2,0.2f);
        information.SetActive(true);
    }
    public void PointerUp()
    {
        
        information.SetActive(false);
    }
}
