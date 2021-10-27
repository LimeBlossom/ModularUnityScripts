using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScalar : MonoBehaviour
{
    public float timeScale;

    private void Start()
    {
        Time.timeScale = timeScale;
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach(AudioSource source in audioSources)
        {
            source.pitch = timeScale;
        }
    }
}
