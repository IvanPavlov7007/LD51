using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using System;

public class ShroomAttacker : ShroomComponent
{
    protected override void onPunish()
    {
    }

    protected override void onReady()
    {
        Tween.Position(transform, transform.position + Vector3.right * 20, 2f, 0f, Tween.EaseIn);
        Run.After(1f, () => { GameManager.instance.enemy.health.Hit((int)Math.Pow(10, shroom.lifes - 1)); });
        Destroy(gameObject, 2f);
    }

    protected override void onReward()
    {
    }
}
