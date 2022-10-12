using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    public double atackTime;
    public int damage;

    public Health health { get; private set; }

    CompositeDisposable disposables = new CompositeDisposable();

    private void Start()
    {
        health = GetComponent<Health>();
        Observable
            .Interval(TimeSpan.FromSeconds(atackTime))
            .Subscribe(x => {
                GameManager.instance.baseHealth.Hit(damage);
            })
            .AddTo(disposables);
    }

    private void OnDisable() => disposables.Clear();


}
