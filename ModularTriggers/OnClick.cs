using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnClick : MonoBehaviour
{
    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    [SerializeField] private int buttonNum = 0;

    [SerializeField] private bool onDown = false;
    [SerializeField] private bool onUp = false;
    [SerializeField] private bool whileDown = false;

    void Update()
    {
        if(whileDown && Input.GetMouseButton(buttonNum))
        {
            Activate();
        }
        if(onDown && Input.GetMouseButtonDown(buttonNum))
        {
            Activate();
        }
        if(onUp && Input.GetMouseButtonUp(buttonNum))
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
