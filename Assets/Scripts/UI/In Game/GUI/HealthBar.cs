using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    Slider slider;
    [SerializeField]
    Health health;

    void Start()
    {
        slider = GetComponent<Slider>();
        if (health == null)
            health = GetComponentInParent<Health>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        int lifes = health.amount;
        int maxLifes = health.maxAmount;
        slider.value = lifes / (float)maxLifes;
    }
}
