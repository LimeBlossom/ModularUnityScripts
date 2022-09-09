using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnKey : MonoBehaviour
{
    [SerializeField] private string[] keys;
    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    [SerializeField] private bool onDown = false;

    void Update()
    {
        bool canActivate = false;
        foreach(string key in keys)
        {
            if (!onDown && Input.GetKey(key))
            {
                canActivate = true;
                break;
            }
            else if(onDown && Input.GetKeyDown(key))
            {
                canActivate = true;
                break;
            }
        }

        if(canActivate)
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
