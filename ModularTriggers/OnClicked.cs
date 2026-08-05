using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnClicked : MonoBehaviour, IClickable
{
    [SerializeField] private int buttonNum = 0;
    [SerializeField] private UnityEvent onClickEvents;
    [SerializeField] private MonoBehaviour[] onClickActions;
    [SerializeField] private bool debug;
    private bool mouseIsOver;

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

    public void Click()
    {
        if(debug)
        {
            print(name + " clicked");
        }
        ActivateOnClick();
    }
}
