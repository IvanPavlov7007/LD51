using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShroomTimerDisplayer : MonoBehaviour
{
    TextMeshPro textMeshPro;
    TimerWithAFrame timer;
    void Start()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        timer = GetComponentInParent<TimerWithAFrame>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        textMeshPro.text = string.Format("{0:0.00}", timer.elapsedTime);
    }
}
