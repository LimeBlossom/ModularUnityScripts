using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessageString : MonoBehaviour, IActivatable
{
    [SerializeField] private StringReference type;
    [SerializeField] private StringReference message;

    public void Activate()
    {
        MessageCenter.InvokeMessage(type.value, message.value);
    }
}
