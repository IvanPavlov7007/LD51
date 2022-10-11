using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class ShroomWorker : ShroomComponent
{
    protected override void onPunish()
    {
    }

    protected override void onReady()
    {
        Tween.Spline(GetComponentInChildren<Spline>(), transform, 0f, 1f, true, 0.5f, 0f, Tween.EaseIn);
        Run.After(0.1f, () => { GameManager.instance.money += shroom.lifes / 4.0; });
        Destroy(gameObject, 1f);
    }

    protected override void onReward()
    {
    }
}
