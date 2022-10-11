using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;

public class Metronome : MonoBehaviour
{
    AudioSource aud;
    [SerializeField]
    AudioClip sound;

    CompositeDisposable disposables = new CompositeDisposable();

    void Start()
    {
        aud = GetComponent<AudioSource>();
         Observable
            .Interval(TimeSpan.FromSeconds(0.5))
            .Subscribe(x => {
                aud.PlayOneShot(sound);
            })
            .AddTo(disposables);
    }

    private void OnDisable() => disposables.Clear();
}
