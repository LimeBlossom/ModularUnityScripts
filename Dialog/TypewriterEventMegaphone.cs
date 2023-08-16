using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypewriterEventMegaphone : MonoBehaviour
{
    public Febucci.UI.Core.TypewriterCore typewriter;

    //Adds and removes listening to callback
    void OnEnable() => typewriter.onMessage.AddListener(OnTypewriterMessage);
    void OnDisable() => typewriter.onMessage.RemoveListener(OnTypewriterMessage);

    //Does stuff based on event
    void OnTypewriterMessage(Febucci.UI.Core.Parsing.EventMarker eventMarker)
    {
        MessageCenter.InvokeMessage(eventMarker.name, "");
    }
}
