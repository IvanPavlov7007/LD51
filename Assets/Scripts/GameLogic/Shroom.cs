using System.Collections;
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

    protected void Start()
    {
        timer = GetComponent<TimerWithAFrame>();
        visualStateMachine = GetComponentInChildren<StateMachine>();
        timer.onFrameTimeout += FrameTimeOut;
        timer.onFrameEnter += FrameEnter;
        currentState = ShroomState.Upcomming;
        displayState();

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

    void reward()
    {
        displayHint = false;
        GameManager.instance.AddScore(1);
    }

    void punish()
    {
        displayHint = true;
        GameManager.instance.RemoveLife();
        visualStateMachine.ChangeState("crime");
        StartCoroutine(visualPunishment());
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