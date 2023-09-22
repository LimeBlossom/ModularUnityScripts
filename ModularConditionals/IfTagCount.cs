using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IfTagCount : MonoBehaviour, IActivatable
{
    [SerializeField] private string tagToCount;

    [SerializeField] private float isSmallerThan = float.NegativeInfinity;
    [SerializeField] private float isEqualTo = float.PositiveInfinity;
    [SerializeField] private float isLargerThan = float.PositiveInfinity;

    [SerializeField] private bool onUpdate = false;

    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    void Update()
    {
        if(onUpdate)
        {
            Activate();
        }
    }

    public void ActivateActions()
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

    public void Activate()
    {
        int toCheck = GameObject.FindGameObjectsWithTag(tagToCount).Length;
        if (isSmallerThan > float.NegativeInfinity)
        {
            if (toCheck < isSmallerThan)
            {
                ActivateActions();
            }
        }
        if (isEqualTo < float.PositiveInfinity)
        {
            if (toCheck == isEqualTo)
            {
                ActivateActions();
            }
        }
        if (isLargerThan < float.PositiveInfinity)
        {
            if (toCheck > isLargerThan)
            {
                ActivateActions();
            }
        }
    }
}
