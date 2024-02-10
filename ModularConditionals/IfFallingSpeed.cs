using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IfFallingSpeed : MonoBehaviour, IActivatable
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float isLargerThan = -1;
    [SerializeField] private float isSmallerThan = -1;

    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    [SerializeField] private bool debug;

    public void Activate()
    {
        if(debug)
        {
            Debug.Log("Falling speed is " + rb.velocity.y);
        }
        
        if(isLargerThan > -1)
        {
            if(rb.velocity.y < 0)
            {
                if (rb.velocity.y * -1 > isLargerThan)
                {
                    if (debug)
                    {
                        Debug.Log("Activating.");
                    }
                    ActivateActions();
                }
            }
        }
        if(isSmallerThan > -1)
        {
            if (rb.velocity.y < 0)
            {
                if (rb.velocity.y * -1 < isSmallerThan)
                {
                    ActivateActions();
                }
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
}
