using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCameraSize : MonoBehaviour, IActivatable
{
    [SerializeField] private Camera targetCamera;
    [SerializeField] private float desiredSize;
    [SerializeField] private float time;
    [SerializeField] private bool onAwake;


    private void Awake()
    {
        if(onAwake)
        {
            Activate();
        }
    }

    public void Activate()
    {
        StartCoroutine(LerpSize(desiredSize, time));
    }

    IEnumerator LerpSize(float targetSize, float duration)
    {
        float time = 0;
        float startSize = targetCamera.orthographicSize;

        while (time < duration)
        {
            targetCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        targetCamera.orthographicSize = targetSize;
    }
}
