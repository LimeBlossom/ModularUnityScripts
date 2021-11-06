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
    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    private float timer;
    private bool ranOnce = false;

    public void Activate()
    {
        if(onlyRunOnce && ranOnce)
        {
            return;
        }
        ranOnce = true;
        if(alwaysRandomizerTimer)
        {
            timer = Random.Range(activateAfterTime.min, activateAfterTime.max);
        }
        StartCoroutine(CountDown(timer));
    }

    void Awake()
    {
        timer = Random.Range(activateAfterTime.min, activateAfterTime.max);

        if (startTimerOnAwake)
        {
            Activate();
        }
    }

    IEnumerator CountDown(float duration)
    {
        yield return new WaitForSeconds(timer);
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
