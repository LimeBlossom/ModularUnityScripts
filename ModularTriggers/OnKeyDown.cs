using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnKeyDown : MonoBehaviour
{
    [SerializeField] private string[] keys;
    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    void Update()
    {
        bool canActivate = false;
        foreach (string key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                canActivate = true;
            }
        }

        if (canActivate)
        {
            Activate();
        }
    }

    public void Activate()
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
