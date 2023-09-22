using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventCaller : MonoBehaviour, IActivatable
{
    [SerializeField] private GameEvent[] events;

    public void Activate()
    {
        foreach(GameEvent e in events)
        {
            e.Raise();
        }
    }
}
