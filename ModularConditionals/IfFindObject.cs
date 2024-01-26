using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IfFindObject : MonoBehaviour, IActivatable
{
    [SerializeField] private string tagToFind;
    [SerializeField] private bool ifNotFind;
    [SerializeField] private bool findAll;

    [SerializeField] private MonoBehaviour[] gameObjectSettables;
    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    public void Activate()
    {
        if(findAll)
        {
            GameObject[] found = GameObject.FindGameObjectsWithTag(tagToFind);
            if(found.Length > 0)
            {
                ActivateActions(found);
            }
            else if (ifNotFind)
            {
                ActivateActions();
            }
        }
        else
        {
            GameObject found = GameObject.FindGameObjectWithTag(tagToFind);
            if (found != null)
            {
                ActivateActions(found);
            }
            else if (ifNotFind)
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

    private void ActivateActions(GameObject obj)
    {
        if (obj != null &&
            gameObjectSettables != null &&
            gameObjectSettables.Length > 0)
        {
            foreach (ISettableGameObject settable in gameObjectSettables)
            {
                settable.SetGameObject(obj);
            }
        }
        events.Invoke();
        if (actions != null && actions.Length > 0)
        {
            foreach (IActivatable action in actions)
            {
                action.Activate();
            }
        }
    }

    private void ActivateActions(GameObject[] objs)
    {
        if (objs != null &&
            gameObjectSettables != null &&
            gameObjectSettables.Length > 0)
        {
            foreach (ISettableGameObject settable in gameObjectSettables)
            {
                foreach(GameObject obj in objs)
                {
                    settable.SetGameObject(obj);
                }
            }
        }
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
