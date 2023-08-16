using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IfRandom : MonoBehaviour, IActivatable
{
    [SerializeField] private float percentChance;

    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;
    [SerializeField] private UnityEvent elseEvents;
    [SerializeField] private MonoBehaviour[] elseActions;

    public void Activate()
    {
        if(Random.Range(0,100) < percentChance)
        {
            ActivateActions();
        }
        else
        {
            ActivateElseActions();
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

    private void ActivateElseActions()
    {
        elseEvents.Invoke();
        if (elseActions != null && elseActions.Length > 0)
        {
            foreach (IActivatable action in elseActions)
            {
                action.Activate();
            }
        }
    }
}
