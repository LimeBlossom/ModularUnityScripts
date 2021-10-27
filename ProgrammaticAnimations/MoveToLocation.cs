using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLocation : MonoBehaviour, IActivatable
{
    public Vector3 positionToMoveTo;
    public float timeToMove;
    [SerializeField] private bool onStart = false;
    [SerializeField] private bool localPosition = false;

    public void Activate()
    {
        StartCoroutine(LerpPosition(positionToMoveTo, timeToMove));
    }

    private void Start()
    {
        if(onStart)
        {
            StartCoroutine(LerpPosition(positionToMoveTo, timeToMove));
        }
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        if(localPosition)
        {
            startPosition = transform.localPosition;
        }

        while (time < duration)
        {
            if(localPosition)
            {
                transform.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            }
            else
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            }
            
            time += Time.deltaTime;
            yield return null;
        }
        if(localPosition)
        {
            transform.localPosition = targetPosition;
        }
        else
        {
            transform.position = targetPosition;
        }
    }
}
