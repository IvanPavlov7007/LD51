using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShroomComponent : MonoBehaviour
{
    protected Shroom shroom;
    protected virtual void Start()
    {
        shroom = GetComponent<Shroom>();
        shroom.onReward += onReward;
        shroom.onReward += onPunish;
        shroom.onReady += onReady;
        shroom.onPunishMiss += onPunishMiss;
    }
    protected abstract void onReward();
    protected abstract void onPunish();

    protected abstract void onReady();

    protected abstract void onPunishMiss();
}
