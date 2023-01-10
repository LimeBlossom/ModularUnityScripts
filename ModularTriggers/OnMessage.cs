using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnMessage : MonoBehaviour
{
    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    [SerializeField] private string type;
    [SerializeField] private bool debug;

    void OnEnable()
    {
        MessageCenter.OnUserMessage += CheckMessage;
    }

    void OnDisable()
    {
        MessageCenter.OnUserMessage -= CheckMessage;
    }

    private void CheckMessage(string type, User user, string message)
    {
        if(debug)
        {
            Debug.Log("Received Message.");
        }
        if(type == this.type)
        {
            if(debug)
            {
                Debug.Log("Activated via message.");
            }
            Activate();
        }
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
