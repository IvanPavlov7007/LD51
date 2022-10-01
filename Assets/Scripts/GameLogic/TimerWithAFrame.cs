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

    float halfFrameTime;
    public float elapsedTime { get; private set; }

    bool iterationTimeoutTriggered;
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
            iterationTimeoutTriggered = false;
            elapsedTime -= timerTick;
        }    

        if(elapsedTime > halfFrameTime && !iterationTimeoutTriggered)
        {
            iterationTimeoutTriggered = true;
            if(onFrameTimeout != null)
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
        return (elapsedTime < halfFrameTime || elapsedTime > timerTick - halfFrameTime);
    }
}
