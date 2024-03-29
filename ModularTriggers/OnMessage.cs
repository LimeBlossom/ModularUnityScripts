using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnMessage : MonoBehaviour
{
    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    [SerializeField] private StringReference type;
    [SerializeField] private StringReference message;
    [SerializeField] private bool debug;

    void OnEnable()
    {
        MessageCenter.OnMessage += CheckMessage;
    }

    void OnDisable()
    {
        MessageCenter.OnMessage -= CheckMessage;
    }

    private void CheckMessage(string type, string message)
    {
        if(debug)
        {
            Debug.Log("Received Message.");
        }
        if(type == this.type.value)
        {
            if(this.message.value == "" || this.message.value == message)
            {
                if (debug)
                {
                    Debug.Log("Activated via message.");
                }
                Activate();
            }
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
