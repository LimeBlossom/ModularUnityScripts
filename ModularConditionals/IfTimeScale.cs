using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IfTimeScale : MonoBehaviour, IActivatable
{
    [SerializeField] private float isSmallerThan = float.NegativeInfinity;
    [SerializeField] private float isEqualTo = float.PositiveInfinity;
    [SerializeField] private float isLargerThan = float.PositiveInfinity;

    [SerializeField] private bool onUpdate = true;

    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    public void Activate()
    {
        if (isSmallerThan > float.NegativeInfinity)
        {
            if (Time.timeScale < isSmallerThan)
            {
                ActivateActions();
            }
        }
        if (isEqualTo < float.PositiveInfinity)
        {
            if (Time.timeScale == isEqualTo)
            {
                ActivateActions();
            }
        }
        if (isLargerThan < float.PositiveInfinity)
        {
            if (Time.timeScale > isLargerThan)
            {
                ActivateActions();
            }
        }
    }

    void Update()
    {
        if (onUpdate)
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
