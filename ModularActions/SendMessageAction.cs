using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessageAction : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject target;
    [SerializeField] private string functionName;
    [SerializeField] private object toSend;

    public void Activate()
    {
        if(target != null)
        {
            if (toSend != null)
            {
                target.SendMessage(functionName, toSend);
            }
            else
            {
                target.SendMessage(functionName);
            }
        }
    }
}
