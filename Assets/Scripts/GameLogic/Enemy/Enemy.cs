using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public int initialLifes;
    public int lifes { get; private set; }

    public Action onDeath;

    private void Awake()
    {
        lifes = initialLifes;
    }

    public void Hit(int count)
    {
        lifes -= count;
        if(lifes <= 0)
        {
            if (onDeath != null)
                onDeath();
        }
    }
}
