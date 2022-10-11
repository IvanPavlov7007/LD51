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

    public event Action onPunish;
    public event Action onReward;

    protected void Start()
    {
        body = transform.Find("body");
        timer = GetComponent<TimerWithAFrame>();
        visualStateMachine = GetComponentInChildren<StateMachine>();
        timer.onFrameTimeout += FrameTimeOut;
        timer.onFrameEnter += FrameEnter;
        currentState = ShroomState.Upcomming;
        displayState();

        if(BeatManager.instance != null)
            BeatManager.instance.registerNewShroom(this);
    }

    public void Click()
    {
        if(timer.CheckIfInFrame() && !currentIterationClicked && currentIteration > 0)
        {
            reward();
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
        currentIterationClicked = false;
    }

    public void FrameEnter()
    {
            if(displayHint)
        currentState = ShroomState.Hitnow;
        displayState();
    }

    int lifes = 2;
    int tries = 1;

    void reward()
    {
        if (onReward != null)
            onReward();
        tries--;
        lifes++;
        if (lifes > 4 || tries < 0)
            Launch();
        Tween.LocalScale(body, body.localScale * 1.2f, 0.5f, 0f, Tween.EaseSpring);
        displayHint = false;
        GameManager.instance.AddScore(1);
    }

    void punish()
    {
        if(tries < 0)

        if (onPunish != null)
            onPunish();
        tries--;
        lifes--;
        if (lifes < 0)
            Destroy(gameObject, 0.5f);

        if (tries < 0)
            Launch();

        Tween.LocalScale(body, body.localScale * 5f / 6f, 0.5f, 0f, Tween.EaseSpring);
        //timer.Reset();
        
        displayHint = true;
        GameManager.instance.RemoveLife();
        visualStateMachine.ChangeState("crime");
        StartCoroutine(visualPunishment());
    }

    void Launch()
    {
        Tween.Position(transform, transform.position + Vector3.right * 20, 2f, 0f, Tween.EaseIn);
        Run.After(1f, () => { GameManager.instance.enemy.Hit((int)Math.Pow(10, lifes - 1)); });
        Destroy(gameObject, 2f);
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

public enum ShroomState { Default, Upcomming, Hitnow}