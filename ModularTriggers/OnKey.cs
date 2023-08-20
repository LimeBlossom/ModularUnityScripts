using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnKey : MonoBehaviour
{
    [SerializeField] private KeyCode[] keys;
    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    [SerializeField] private bool onDown = false;
    [SerializeField] private bool onUp = false;

    void Update()
    {
        bool canActivate = false;
        foreach(KeyCode key in keys)
        {
            if (!onDown && !onUp && Input.GetKey(key))
            {
                canActivate = true;
                break;
            }
            else if(onDown && Input.GetKeyDown(key))
            {
                canActivate = true;
                break;
            }
            else if(onUp && Input.GetKeyUp(key))
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
