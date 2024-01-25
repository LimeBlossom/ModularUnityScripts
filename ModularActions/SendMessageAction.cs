using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessageAction : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject[] targets;
    [SerializeField] private StringReference functionName;
    [SerializeField] private object toSend;

    public void Activate()
    {
        foreach(GameObject target in targets)
        {
            if (toSend != null)
            {
                target.SendMessage(functionName.value, toSend);
            }
            else
            {
                target.SendMessage(functionName.value);
            }
        }
    }
}
