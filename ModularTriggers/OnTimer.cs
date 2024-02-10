using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTimer : MonoBehaviour, IActivatable
{
    [SerializeField] private bool startTimerOnAwake;
    [SerializeField] private bool onlyRunOnce = false;
    [SerializeField] private bool alwaysRandomizerTimer;
    [SerializeField] private MinMaxFloat activateAfterTime;
    [SerializeField] private int maxRuns = int.MaxValue;
    private int runs;
        
    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;
    [SerializeField] private bool unscaledTime = false;

    [SerializeField] private float timer = 10;

    void Awake()
    {
        timer = Random.Range(activateAfterTime.min, activateAfterTime.max);
        if (startTimerOnAwake)
        {
            Activate();
        }
    }

    public void Activate()
    {
        if(onlyRunOnce && runs > 0)
        {
            return;
        }
        if(maxRuns < int.MaxValue && runs > maxRuns)
        {
            return;
        }

        runs++;

        if(alwaysRandomizerTimer)
        {
            timer = Random.Range(activateAfterTime.min, activateAfterTime.max);
        }
        if(enabled)
        {
            StartCoroutine(CountDown(timer));
        }
    }

    IEnumerator CountDown(float duration)
    {
        if(unscaledTime)
        {
            yield return new WaitForSecondsRealtime(duration);
        }
        else
        {
            yield return new WaitForSeconds(duration);
        }
        ActivateActions();
        if(startTimerOnAwake)
        {
            Activate();
        }
    }

    private void ActivateActions()
    {
        events.Invoke();
        if (actions != null && actions.Length > 0)
        {
            foreach (IActivatable action in actions)
            {
                action.Activate();
            }
        }
    }
}
