using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MessageCenter
{
    public delegate void Message(string type, string message);
    public static event Message OnMessage;

    public delegate void UserMessage(string type, TwitchUser user, string message);
    public static event UserMessage OnUserMessage;

    public static void InvokeMessage(string type, string message = "")
    {
        OnMessage?.Invoke(type, message);
        //Debug.Log($"Invoked Message: {type} {message}");
    }

    public static void InvokeUserMessage(string type, TwitchUser user, string message = "")
    {
        OnUserMessage?.Invoke(type, user, message);
    }
}