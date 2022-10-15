using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerWithAFrame : ClampedTimer
{

    [SerializeField]
    float initialTimeFrame = 0.5f;
    public float timeFrame { get; set; }

    public event System.Action onFrameTimeout;
    public event System.Action onFrameEnter;


    float halfFrameTime;
    public bool currentlyInFrame { get; private set; }

    bool iterationEnterTimeoutTriggered;
    bool iterationExitTimeoutTriggered;
    protected override void Awake()
    {
        base.Awake();
        currentlyInFrame = true;
        elapsedTime = 0f;
        timeFrame = initialTimeFrame;
        halfFrameTime = timeFrame / 2f;
    }


    //bool firstTimeDone = false;
    protected override void Update()
    {
        if (paused)
            return;
        elapsedTime += Time.deltaTime;
        if (elapsedTime > timerTick)
        {
            iterationExitTimeoutTriggered = false;
            iterationEnterTimeoutTriggered = false;
            elapsedTime -= timerTick;
        }    

        if(elapsedTime >= timerTick - halfFrameTime && !iterationEnterTimeoutTriggered)
        {
            iterationEnterTimeoutTriggered = true;
            currentlyInFrame = true;
            if (onFrameEnter != null)
                onFrameEnter();
        }

        if(elapsedTime > halfFrameTime && !iterationExitTimeoutTriggered)
        {
            iterationExitTimeoutTriggered = true;
            currentlyInFrame = false;
            if (onFrameTimeout != null)
                onFrameTimeout();
        }

    }

    public virtual void Reset()
    {
        elapsedTime = 0f;
        iterationExitTimeoutTriggered = false;
        iterationEnterTimeoutTriggered = false;
        Continue();
    }

    public virtual void Continue()
    {
        paused = false;
    }

    public virtual void Pause()
    {
        paused = true;
    }

    public virtual bool CheckIfInFrame()
    {
        if (paused)
            Debug.Log("paused: " + gameObject.name);
        return currentlyInFrame;//(elapsedTime < halfFrameTime || elapsedTime > timerTick - halfFrameTime);
    }
}
