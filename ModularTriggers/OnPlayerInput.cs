using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class OnPlayerInput : MonoBehaviour
{
    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    private void Activate()
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

    private void OnPause()
    {
        Activate();
    }

    private void OnMove()
    {
        Activate();
    }

    private void OnGrab()
    {
        Activate();
    }

    private void OnFinishLevel()
    {
        Activate();
    }

    private void OnTimeBurst()
    {
        Activate();
    }

    private void OnRotateItemCW()
    {
        Activate();
    }

    private void OnRotateItemCCW()
    {
        Activate();
    }

    private void OnRotatePlayer()
    {
        Activate();
    }
}
