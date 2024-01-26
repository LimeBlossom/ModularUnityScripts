using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnStart : MonoBehaviour
{
    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    private void Start()
    {
        if(events != null)
        {
            events.Invoke();
        }
        if (actions.Length > 0)
        {
            foreach (IActivatable action in actions)
            {
                action.Activate();
            }
        }
    }
}