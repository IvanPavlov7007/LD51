using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShroomSound : MonoBehaviour
{
    [SerializeField]
    AudioClip sound;
    AudioSource aud;
    Shroom shroom;
    void Start()
    {
        shroom = GetComponent<Shroom>();
        aud = GetComponent<AudioSource>();
        shroom.onReward += onReward;
        aud.PlayOneShot(sound);
    }

    void onReward()
    {
        aud.PlayOneShot(sound);
    }
}
