using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampedTimer : MonoBehaviour
{
    [SerializeField]
    float initialTimerTick = 10f;
    public float timerTick { get; set; }

    public float elapsedTime { get; protected set; }

    [SerializeField]
    bool startOnAwake;

    public bool paused { get; protected set; }

    protected virtual void Awake()
    {
        elapsedTime = 0f;
        timerTick = initialTimerTick;
        paused = !startOnAwake;
    }
    protected virtual void Update()
    {
        if (paused)
            return;

        elapsedTime += Time.deltaTime;
        if (elapsedTime > timerTick)
        {
            elapsedTime -= timerTick;
        }
    }
}