using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IfFloatVariable : MonoBehaviour, IActivatable
{
    [SerializeField] private float isSmallerThan = float.NegativeInfinity;
    [SerializeField] private float isEqualTo = float.PositiveInfinity;
    [SerializeField] private float isLargerThan = float.PositiveInfinity;

    [SerializeField] private FloatVariable toCheck;

    [SerializeField] private bool onUpdate = true;

    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    public void Activate()
    {
        if(isSmallerThan > float.NegativeInfinity)
        {
            if(toCheck.value < isSmallerThan)
            {
                ActivateActions();
            }
        }
        if(isEqualTo < float.PositiveInfinity)
        {
            if(toCheck.value == isEqualTo)
            {
                ActivateActions();
            }
        }
        if(isLargerThan < float.PositiveInfinity)
        {
            if(toCheck.value > isLargerThan)
            {
                ActivateActions();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(onUpdate)
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
