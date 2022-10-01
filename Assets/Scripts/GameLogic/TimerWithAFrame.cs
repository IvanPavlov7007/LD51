using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerWithAFrame : MonoBehaviour
{
    [SerializeField]
    float initialTimerTick = 10f;
    public float timerTick { get; set; }

    [SerializeField]
    float initialTimeFrame = 0.5f;
    public float timeFrame { get; set; }

    [SerializeField]
    bool startOnAwake;
    public bool paused { get; private set; }

    public event System.Action onFrameTimeout;
    public event System.Action onFrameEnter;


    float halfFrameTime;
    public float elapsedTime { get; private set; }
    public bool currentlyInFrame { get; private set; }

    bool iterationEnterTimeoutTriggered;
    bool iterationExitTimeoutTriggered;
    protected virtual void Awake()
    {
        elapsedTime = 0f;
        timerTick = initialTimerTick;
        timeFrame = initialTimeFrame;
        paused = !startOnAwake;
        halfFrameTime = timeFrame / 2f;
    }


    //bool firstTimeDone = false;
    protected virtual void Update()
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
