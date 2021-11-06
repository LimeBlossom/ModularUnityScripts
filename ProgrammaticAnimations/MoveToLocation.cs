using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLocation : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject toMove;

    public Vector3 positionToMoveTo;
    public float timeToMove;
    [SerializeField] private bool localPosition = false;

    private void Start()
    {
        if(toMove == null)
        {
            toMove = gameObject;
        }
    }

    public void Activate()
    {
        StartCoroutine(LerpPosition(positionToMoveTo, timeToMove));
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
            
            time += Time.deltaTime;
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
