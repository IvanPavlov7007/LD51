using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class Shroom : MonoBehaviour
{
    ShroomState currentState;
    StateMachine visualStateMachine;
    TimerWithAFrame timer;
    bool currentIterationClicked = false;
    bool displayHint = true;
    int currentIteration;
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
        currentState = ShroomState.Upcomming;

        ShroomPool.getShroomClass(shroomType).shrooms.Add(this);

        lifes = 3;
        tries = 4;

        displayState();

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
        if(timer.CheckIfInFrame())
        {
            if(!currentTimeFrameClicked)
                reward();
            currentTimeFrameClicked = true;
            currentState = ShroomState.Default;
            displayState();
        }
        else
        {
            punish();
            currentState = ShroomState.Upcomming;
        }
        currentIterationClicked = true;
    }

    public void FrameTimeOut()
    {
        if (currentIteration > 0)
        {
            currentState = ShroomState.Default;
            if (!currentIterationClicked)
            {
                punish();
            }
            else
            {
                displayState();
            }
        }
        currentIteration++;
        currentTimeFrameClicked = false;
        currentIterationClicked = false;
    }

    public void FrameEnter()
    {
            if(displayHint)
        currentState = ShroomState.Hitnow;
        displayState();
    }

    public int lifes { get; private set; }
    public int tries { get; private set; }

    void reward()
    {
        if (onReward != null)
            onReward();
        //tries--;
        lifes++;
        if (lifes > 5 || tries < 0)
            Launch();
        Tween.LocalScale(body, body.localScale * 1.2f, 0.5f, 0f, Tween.EaseSpring);
        displayHint = false;
        GameManager.instance.AddScore(1);
    }

    void punish()
    {
        if (onPunish != null)
            onPunish();
        //tries--;
        lifes--;
        if (lifes < 1)
            Destroy(gameObject, 0.5f);
        else if (tries < 0)
            Launch();

        Tween.LocalScale(body, body.localScale * 5f / 6f, 0.5f, 0f, Tween.EaseSpring);
        //timer.Reset();
        
        displayHint = true;
        GameManager.instance.RemoveLife();
        visualStateMachine.ChangeState("crime");
        StartCoroutine(visualPunishment());
        currentState = ShroomState.Upcomming;
    }

    void punishMiss()
    {
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
        yield return new WaitForSeconds(0.2f);
        displayState();
    }

    void displayState()
    {
        visualStateMachine.ChangeState((int)currentState);
    }
    

    private void OnMouseDown()
    {
        Click();
    }
}
[Serializable]
public enum ShroomType { Worker, Attacker }


public enum ShroomState { Default, Upcomming, Hitnow}