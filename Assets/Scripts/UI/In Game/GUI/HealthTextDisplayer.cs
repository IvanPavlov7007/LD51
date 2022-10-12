using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthTextDisplayer : PropertyDisplayer
{
    [SerializeField]
    Health health;

    protected override void Start()
    {
        base.Start();
        if (health == null)
            health = GetComponentInParent<Health>();
    }

    void LateUpdate()
    {
        int lifes = health.amount;
        int maxLifes = health.maxAmount;
        textMeshPro.text = lifes.ToString() + " / " + maxLifes.ToString();
    }
}
