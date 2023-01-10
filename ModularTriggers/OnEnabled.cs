using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnabled : MonoBehaviour
{
    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    private void OnEnable()
    {
        Activate();
    }

    public void Activate()
    {
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
