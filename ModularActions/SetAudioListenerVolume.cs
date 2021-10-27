using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAudioListenerVolume : MonoBehaviour, IActivatable, ISettableFloat
{
    [SerializeField] private float desiredVolume;

    public void SetFloat(float setTo)
    {
        desiredVolume = setTo;
    }

    public void Activate()
    {
        AudioListener.volume = desiredVolume;
    }
}
