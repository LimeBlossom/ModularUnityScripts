using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnUpdate : MonoBehaviour
{
    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    [SerializeField] private bool fixedUpdate = false;
    [SerializeField] private bool lateUpdate = false;

    private void Update()
    {
        if (fixedUpdate || lateUpdate) return;
        events.Invoke();
        if (actions.Length > 0)
        {
            foreach (IActivatable action in actions)
            {
                action.Activate();
            }
        }
    }

    private void LateUpdate()
    {
        if (!lateUpdate) return;
        events.Invoke();
        if (actions.Length > 0)
        {
            foreach (IActivatable action in actions)
            {
                action.Activate();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!fixedUpdate) return;
        events.Invoke();
        if (actions.Length > 0)
        {
            foreach (IActivatable action in actions)
            {
                action.Activate();
            }
        }
    }
}
