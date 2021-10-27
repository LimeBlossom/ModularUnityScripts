using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent[] events;
    public UnityEvent response;

    private void OnEnable()
    {
        foreach(GameEvent gameEvent in events)
        { gameEvent.RegisterListener(this); }
    }

    private void OnDisable()
    {
        foreach(GameEvent gameEvent in events)
        { gameEvent.UnregisterListener(this); }
    }

    public void OnEventRaised()
    {
        response.Invoke();
    }
}
