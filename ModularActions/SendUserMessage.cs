using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendUserMessage : MonoBehaviour, IActivatable
{
    [SerializeField] private TwitchUser user;
    [SerializeField] private string type;
    [SerializeField] private string message;

    public void Activate()
    {
        MessageCenter.InvokeUserMessage(type, user, message);
    }
}
