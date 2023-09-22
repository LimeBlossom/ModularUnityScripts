using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnClicked : MonoBehaviour
{
    [SerializeField] private int buttonNum = 0;
    [SerializeField] private UnityEvent onClickEvents;
    [SerializeField] private MonoBehaviour[] onClickActions;
    [SerializeField] private bool debug;
    private bool mouseIsOver;

    private void Update()
    {
        if(mouseIsOver && Input.GetMouseButtonDown(buttonNum))
        {
            ActivateOnClick();
        }
    }

    private void OnMouseOver()
    {
        if (debug)
        {
            print("OnMouseOver");
        }
        mouseIsOver = true;
    }

    private void OnMouseExit()
    {
        mouseIsOver = false;
    }

    void ActivateOnClick()
    {
        onClickEvents.Invoke();
        if (onClickActions != null && onClickActions.Length > 0)
        {
            foreach (IActivatable action in onClickActions)
            {
                action.Activate();
            }
        }
    }
}
