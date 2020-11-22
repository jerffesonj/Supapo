using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    public static MenuAudio instance;
    [SerializeField] AudioSource audioSource;
    //public AudioClip songMenu1;
    //public AudioClip songMenu2;
    //public AudioClip songMenu3;

    [SerializeField] List<AudioClip> audioClips;
    [SerializeField] AudioClip selectSFX;

    public AudioClip jab;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            StartCoroutine(PlaySong());
        }
    }

    private IEnumerator PlaySong()
    {
        yield return new WaitForSeconds(1f);
        int randomIndex = Mathf.RoundToInt(Random.Range(0, audioClips.Count));
        print(randomIndex);
        audioSource.loop = true;
        audioSource.clip = audioClips[randomIndex];
        audioSource.volume = 0.3f;
        audioSource.Play();
    }

    public AudioSource AudioSource { get { return audioSource; } }

    public AudioClip SelectSFX { get { return selectSFX; } }

}
