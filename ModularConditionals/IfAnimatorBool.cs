using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IfAnimatorBool : MonoBehaviour, IActivatable
{
    [SerializeField] private Animator animator;
    [SerializeField] private string toCheck;

    [SerializeField] private bool isTrue;

    [SerializeField] private bool onUpdate = true;

    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    public void Activate()
    {
        if (animator.GetBool(toCheck) == isTrue)
        {
            ActivateActions();
        }
    }

    void Update()
    {
        if (onUpdate)
        {
            Activate();
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
