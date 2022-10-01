using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifesDisplayer : PropertyDisplayer
{
    void LateUpdate()
    {
        textMeshPro.text = gameManager.currentLifes.ToString();
    }
}
