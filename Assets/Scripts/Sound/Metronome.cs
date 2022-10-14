using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;

//singletone
public class Metronome : MonoBehaviour
{
    AudioSource aud;
    [SerializeField]
    AudioClip sound;

    public event System.Action tick;

    public static Metronome instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    CompositeDisposable disposables = new CompositeDisposable();

    void Start()
    {
        aud = GetComponent<AudioSource>();
         Observable
            .Interval(TimeSpan.FromSeconds(0.5))
            .Subscribe(x => {
                if (tick != null)
                    tick();
                aud.PlayOneShot(sound);
            })
            .AddTo(disposables);
    }

    private void OnDisable() => disposables.Clear();
}
