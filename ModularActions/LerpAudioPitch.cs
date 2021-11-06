using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpAudioPitch : MonoBehaviour, IActivatable
{
    [SerializeField] private AudioSource toLerp;
    [SerializeField] private float finalPitch;
    [SerializeField] private float overTime;

    public void Activate()
    {
        StartCoroutine(LerpValue(finalPitch, overTime));
    }

    IEnumerator LerpValue(float targetValue, float duration = 1)
    {
        float time = 0;
        float startValue = toLerp.pitch;

        while (time < duration)
        {
            toLerp.pitch = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
