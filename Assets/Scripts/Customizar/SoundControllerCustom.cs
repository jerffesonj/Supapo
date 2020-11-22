using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControllerCustom : MonoBehaviour
{
    public static SoundControllerCustom instance;

    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip customSound;
    [SerializeField] AudioClip selectFx1;
    [SerializeField] AudioClip selectFx2;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        PlaySong();
    }

    // Update is called once per frame
    private void PlaySong()
    {
        audioSource.loop = true;
        audioSource.clip = customSound;
        audioSource.volume = 0.3f;
        audioSource.Play();
    }

    public AudioSource AudioSource { get { return audioSource; } }
    public AudioClip SelectedFx1 { get { return selectFx1; } }
    public AudioClip SelectedFx2 { get { return selectFx2; } }
}
