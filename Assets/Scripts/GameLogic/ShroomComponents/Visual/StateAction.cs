using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class StateAction : MonoBehaviour
{

    [SerializeField]
    Color spriteColor;
    private void OnEnable()
    {
        SpriteRenderer sprite = transform.parent.parent.GetComponentInChildren<SpriteRenderer>();
        sprite.color = spriteColor;
    }
}
