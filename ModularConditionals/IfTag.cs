using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IfTag : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject toCheck;
    [SerializeField] private string tagMatch;

    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    public void Activate()
    {
        if (toCheck.CompareTag(tagMatch))
        {
            ActivateActions();
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
