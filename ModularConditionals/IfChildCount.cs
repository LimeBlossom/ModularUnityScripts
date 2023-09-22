using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IfChildCount : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject toCheck;

    [SerializeField] private float isSmallerThan = float.NegativeInfinity;
    [SerializeField] private float isEqualTo = float.PositiveInfinity;
    [SerializeField] private float isLargerThan = float.PositiveInfinity;

    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    public void Activate()
    {
        if (isSmallerThan > float.NegativeInfinity)
        {
            if (toCheck.transform.childCount < isSmallerThan)
            {
                ActivateActions();
            }
        }
        if (isEqualTo < float.PositiveInfinity)
        {
            if (toCheck.transform.childCount == isEqualTo)
            {
                ActivateActions();
            }
        }
        if (isLargerThan < float.PositiveInfinity)
        {
            if (toCheck.transform.childCount > isLargerThan)
            {
                ActivateActions();
            }
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
