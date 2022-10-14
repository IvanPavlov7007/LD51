using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShroomSound : ShroomComponent
{
    [SerializeField]
    AudioClip sound;
    AudioSource aud;
    protected override void Start()
    {
        base.Start();
        shroom = GetComponent<Shroom>();
        aud = GetComponent<AudioSource>();
        aud.PlayOneShot(sound);
    }

    protected override void onReward()
    {
        aud.PlayOneShot(sound);
    }

    protected override void onPunish()
    {
    }

    protected override void onReady()
    {
    }

    protected override void onPunishMiss()
    {

    }
}
