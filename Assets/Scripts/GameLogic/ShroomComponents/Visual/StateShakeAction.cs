using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class StateShakeAction : MonoBehaviour
{
    Transform movable;
    private void OnEnable()
    {
        movable = transform.Find("movable");
        Metronome.instance.tick += Shake;
    }

    void Shake()
    {
        Tween.LocalScale(movable, Vector3.one,Vector3.one * 1.1f, 0.1f, 0f, Tween.EaseWobble);
    }

    private void OnDisable()
    {
        Metronome.instance.tick -= Shake;
    }
}
