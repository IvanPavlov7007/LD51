using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeTracker : MonoBehaviour
{
    Slider slider;
    TextMeshProUGUI textMeshPro;
    Enemy enemy;

    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        int lifes = enemy.lifes;
        int maxLifes = enemy.initialLifes;
        textMeshPro.text = lifes.ToString() + " / " + maxLifes.ToString();
        slider.value = lifes / (float)maxLifes;
    }
}
