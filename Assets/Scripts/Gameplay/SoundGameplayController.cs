using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGameplayController : MonoBehaviour
{
    public static SoundGameplayController instance;

    [Header("Audio Source")]
    public AudioSource audioSource;
    public AudioSource audioSource1;
    public AudioSource fxAudioSource;
    public AudioSource fxAudioSource2;
    public AudioSource dangerAudioSource;

    [Header("Audio Clips")]
    public AudioClip bellStart;
    public AudioClip bellEnd;
    public AudioClip bgSound1;
    public AudioClip bgSound2;
    public AudioClip dodgeSound;
    public AudioClip defendSound;
    public AudioClip perfectDefendSound;
    public AudioClip jabSound;
    public AudioClip diretoSound;
    public AudioClip hookSound;
    public AudioClip upperSound;

    public AudioClip explosionStun;
    public AudioClip dizzy;
    public AudioClip tired;
    public AudioClip beginPunch;
    public AudioClip hitEnemy;
    public AudioClip hitPlayer;
    public AudioClip recharge;
    public AudioClip danger;

    public AudioClip fx;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
