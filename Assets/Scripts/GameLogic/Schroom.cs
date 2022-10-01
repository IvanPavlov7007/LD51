using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class Schroom : MonoBehaviour
{
    StateMachine visualStateMachine;
    TimerWithAFrame timer;
    bool currentIterationClicked = false;
    int currentIteration;

    protected void Start()
    {
        timer = GetComponent<TimerWithAFrame>();
        visualStateMachine = GetComponentInChildren<StateMachine>();
        timer.onFrameTimeout += FrameTimeOut;
    }

    public void Click()
    {
        if(timer.CheckIfInFrame() && !currentIterationClicked && currentIteration > 0)
        {
            reward();
        }
        else
        {
            punish();
        }
        currentIterationClicked = true;
    }

    public void FrameTimeOut()
    {
        if (!currentIterationClicked && currentIteration > 0)
            punish();
        currentIteration++;
        currentIterationClicked = false;
    }

    void reward()
    {
        GameManager.instance.AddScore(1);
    }

    void punish()
    {
        GameManager.instance.RemoveLife();
    }

    

    private void OnMouseDown()
    {
        Click();
    }
}
