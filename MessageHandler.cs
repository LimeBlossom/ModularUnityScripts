using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageHandler : MonoBehaviour
{
    public delegate void Message(string type, int num = 0);
    public static event Message OnMessage;

    public void SendMessage(string type, int num)
    {
        OnMessage?.Invoke(type, num);
    }
}
