using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDisplayer : PropertyDisplayer
{
    float elapsedTime = 0f;
    void Update()
    {
        elapsedTime += Time.deltaTime;
        textMeshPro.SetText(string.Format("{0:0.0}", elapsedTime));
    }
}
