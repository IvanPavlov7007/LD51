using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class StateShakeAction : ShroomComponent
{
    Transform movable;
    private void OnEnable()
    {
        movable = transform.Find("movable");
        Metronome.instance.tick += Shake;
    }

    void Shake()
    {
        if(shroom.currentState != ShroomState.Inactive)
            Tween.LocalScale(movable, Vector3.one,Vector3.one * 1.1f //* Mathf.Sqrt(shroom.lifes)
                , 0.1f, 0f, Tween.EaseWobble);
    }

    private void OnDisable()
    {
        Metronome.instance.tick -= Shake;
    }
}
