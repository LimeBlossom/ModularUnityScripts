using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnButton : MonoBehaviour
{
    [SerializeField] private string[] buttons;
    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    [SerializeField] private bool onDown = false;
    [SerializeField] private bool onUp = false;

    void Update()
    {
        bool canActivate = false;
        foreach (string button in buttons)
        {
            if (!onDown && !onUp && Input.GetButton(button))
            {
                canActivate = true;
                break;
            }
            else if (onDown && Input.GetButtonDown(button))
            {
                canActivate = true;
                break;
            }
            else if (onUp && Input.GetButtonUp(button))
            {
                canActivate = true;
                break;
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
