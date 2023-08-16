using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IfFindObject : MonoBehaviour, IActivatable
{
    [SerializeField] private string tagToFind;
    [SerializeField] private bool ifNotFind;

    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    public void Activate()
    {
        if((GameObject.FindGameObjectWithTag(tagToFind) != null) != ifNotFind)
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
