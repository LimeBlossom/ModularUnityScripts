using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseHoverable : MonoBehaviour, IMouseHoverable
{
    [SerializeField] private UnityEvent onHoverEvents;
    [SerializeField] private MonoBehaviour[] onHoverActions;
    [SerializeField] private UnityEvent onHoverExitEvents;
    [SerializeField] private MonoBehaviour[] onHoverExitActions;
    [SerializeField] private bool debug;
    private bool mouseIsOver;

    

    void ActivateStay()
    {
        onHoverEvents.Invoke();
        if (onHoverActions != null && onHoverActions.Length > 0)
        {
            foreach (IActivatable action in onHoverActions)
            {
                action.Activate();
            }
        }
    }

    void ActivateExit()
    {
        onHoverExitEvents.Invoke();
        if (onHoverExitActions != null && onHoverExitActions.Length > 0)
        {
            foreach (IActivatable action in onHoverExitActions)
            {
                action.Activate();
            }
        }
    }

    public void MouseHoverStay()
    {
        if (debug)
        {
            print(name + " hover");
        }
        ActivateStay();
    }

    public void MouseHoverExit()
    {
        if (debug)
        {
            print(name + " hover");
        }
        ActivateExit();
    }
}
