using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCameraFOV : MonoBehaviour, IActivatable
{
    [SerializeField] private Camera targetCamera;
    [SerializeField] private float desiredFOV;
    [SerializeField] private float time;
    [SerializeField] private bool onAwake;


    private void Awake()
    {
        if (onAwake)
        {
            Activate();
        }
    }

    public void Activate()
    {
        StartCoroutine(LerpFOV(desiredFOV, time));
    }

    IEnumerator LerpFOV(float targetFOV, float duration)
    {
        float time = 0;
        float startSize = targetCamera.fieldOfView;

        while (time < duration)
        {
            targetCamera.fieldOfView = Mathf.Lerp(startSize, targetFOV, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        targetCamera.orthographicSize = targetFOV;
    }
}
