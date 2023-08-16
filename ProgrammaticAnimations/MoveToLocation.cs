using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLocation : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject toMove;

    public Vector3 positionToMoveTo;
    public Transform transformToMoveTo;
    public float timeToMove;
    [SerializeField] private bool localPosition = false;

    public void Activate()
    {
        if(transformToMoveTo == null)
        {
            if (toMove == null)
            {
                toMove = gameObject;
            }
            StartCoroutine(LerpPosition(positionToMoveTo, timeToMove));
        }
        else
        {
            if (toMove == null)
            {
                toMove = gameObject;
            }
            StartCoroutine(LerpPosition(transformToMoveTo.position, timeToMove));
        }
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = toMove.transform.position;
        if(localPosition)
        {
            startPosition = toMove.transform.localPosition;
        }

        while (time < duration)
        {
            if(localPosition)
            {
                toMove.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            }
            else
            {
                toMove.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            }
            
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        if(localPosition)
        {
            toMove.transform.localPosition = targetPosition;
        }
        else
        {
            toMove.transform.position = targetPosition;
        }
    }
}
