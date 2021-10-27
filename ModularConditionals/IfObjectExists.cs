using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IfObjectExists : MonoBehaviour, IActivatable
{
    [SerializeField] private bool doesNotExist;

    [SerializeField] private GameObject[] targets;
    [SerializeField] private string[] targetTags;

    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    private bool objectExists()
    {
        foreach(GameObject target in targets)
        {
            if(target != null)
            {
                return true;
            }
        }
        foreach(string tag in targetTags)
        {
            foreach(GameObject target in GameObject.FindGameObjectsWithTag(tag))
            {
                if(target != null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void Activate()
    {
        bool checkForExist = !doesNotExist;
        if(objectExists() && checkForExist)
        {
            ActivateActions();
        }
        else if(!objectExists() && !checkForExist)
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
