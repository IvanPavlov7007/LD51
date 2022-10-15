using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using Pixelplacement.TweenSystem;

public class Shroom : MonoBehaviour
{
    public ShroomState currentState { get; set; }
    StateMachine visualStateMachine;
    TimerWithAFrame timer;
    Transform body;

    public int price;
    [SerializeField]
    private ShroomType _shroomType;
    public ShroomType shroomType { get { return _shroomType; } private set { _shroomType = value; } }

    public event Action onPunish;
    public event Action onPunishMiss;
    public event Action onReward;
    public event Action onReady;

    public int tweenId;

    private void Awake()
    {

    }

    protected void Start()
    {
        body = transform.Find("movable/body");
        timer = GetComponent<TimerWithAFrame>();
        visualStateMachine = GetComponentInChildren<StateMachine>();
        timer.onFrameTimeout += FrameTimeOut;
        timer.onFrameEnter += FrameEnter;
        currentState = ShroomState.Inactive;

        ShroomPool.getShroomClass(shroomType).shrooms.Add(this);

        lifes = 0;

        if(BeatManager.instance != null)
            BeatManager.instance.registerNewShroom(this);
    }

    private void OnDestroy()
    {
        ShroomPool.getShroomClass(shroomType).shrooms.Remove(this);
    }

    bool currentTimeFrameClicked = false;

    public void Click()
    {
        if (currentState == ShroomState.Inactive)
            reward();
        else if(timer.CheckIfInFrame())
        {
            if(!currentTimeFrameClicked)
                reward();
        }
        else
        {
            punish();
        }
    }

    public void FrameTimeOut()
    {
        if (!currentTimeFrameClicked)
        {
            punishMiss();
        }
    }

    public void FrameEnter()
    {
        currentTimeFrameClicked = false;
    }

    public int lifes { get; private set; }
    public int tries { get; private set; }

    void reward()
    {
        if (lifes < 3)
        {
            lifes++;
            setState(ShroomState.Active);
        }
        
        if (lifes == 3)
        {
            setState(ShroomState.Fever);
        }

        currentTimeFrameClicked = true;

        if (onReward != null)
            onReward();
    }

    void punish()
    {
        setState(ShroomState.Inactive);
        visualStateMachine.ChangeState("crime");
        StartCoroutine(visualPunishment());

        if (onPunish != null)
            onPunish();
    }
    TweenBase lastTween;
    void setState(ShroomState state)
    {

        switch (state)
        {
            case ShroomState.Inactive:
                if (lastTween != null) lastTween.Stop();
                lastTween = Tween.LocalScale(body, Vector3.one, 0.5f, 0f, Tween.EaseSpring);
                lifes = 0;
                timer.Pause();
                break;
            case ShroomState.Active:
                if (lastTween != null) lastTween.Stop();
                lastTween = Tween.LocalScale(body, Vector3.one * Mathf.Pow(1.2f,lifes), 0.5f, 0f, Tween.EaseSpring);
                if (timer.paused)
                    timer.Reset();
                break;
            case ShroomState.Fever:
                if (lastTween != null) lastTween.Stop();
                if (currentState != ShroomState.Fever)
                    lastTween = Tween.LocalScale(body, Vector3.one * Mathf.Pow(1.2f, lifes), 0.5f, 0f, Tween.EaseSpring);
                Launch();
                if (timer.paused)
                    timer.Reset();
                break;
        }
        currentState = state;
    }

    void punishMiss()
    {
        lifes--;
        if (lifes > 0)
        {
            setState(ShroomState.Active);
        }
        else
        {
            punish();
        }

        if (onPunishMiss != null)
            onPunishMiss();
    }

    public void Launch()
    {
        if (onReady != null)
            onReady();
    }

    IEnumerator visualPunishment()
    {
        visualStateMachine.ChangeState("crime");
        yield return new WaitForSeconds(0.2f);
        visualStateMachine.ChangeState("default");
    }
}
[Serializable]
public enum ShroomType { Worker, Attacker }


public enum ShroomState { Inactive, Active, Fever}