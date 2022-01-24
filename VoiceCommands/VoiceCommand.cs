using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VoiceCommand : ScriptableObject
{
    public GameEvent[] eventsToCall;
    public string[] phrases;
}
