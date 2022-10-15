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
        aud = GetComponent<AudioSource>();
        shroom.onReward += onReward;
        shroom.onPunishMiss += onMiss;
    }

    void onMiss()
    {
        aud.PlayOneShot(sound,0.5f);
    }
    
    protected void onReward()
    {
        aud.PlayOneShot(sound);
    }
}
