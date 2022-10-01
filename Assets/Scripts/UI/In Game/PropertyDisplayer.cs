using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PropertyDisplayer : MonoBehaviour
{
    protected TextMeshProUGUI textMeshPro;
    protected GameManager gameManager;

    protected virtual void Start()
    {
        gameManager = GameManager.instance;
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }
}
