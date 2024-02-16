using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnMouseHover : MonoBehaviour
{
    [SerializeField] private UnityEvent onEnterEvents;
    [SerializeField] private MonoBehaviour[] onEnterActions;

    [SerializeField] private UnityEvent onExitEvents;
    [SerializeField] private MonoBehaviour[] onExitActions;

    [SerializeField] private bool debug;

    private void OnMouseOver()
    {
        if(!enabled)
        {
            return;
        }
        if(debug)
        {
            print("OnMouseOver");
        }
        ActivateOnEnter();
    }

    private void OnMouseExit()
    {
        if (!enabled)
        {
            return;
        }
        if (debug)
        {
            print("OnMouseExit");
        }
        ActivateOnExit();
    }


    private void ActivateOnEnter()
    {
        onEnterEvents.Invoke();
        if (onEnterActions.Length > 0)
        {
            foreach (IActivatable action in onEnterActions)
            {
                action.Activate();
            }
        }
    }

    private void ActivateOnExit()
    {
        onExitEvents.Invoke();
        if (onExitActions.Length > 0)
        {
            foreach (IActivatable action in onExitActions)
            {
                action.Activate();
            }
        }
    }
}
