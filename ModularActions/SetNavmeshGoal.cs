using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetNavmeshGoal : MonoBehaviour, IActivatable, ISettableGameObject
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private GameObject targetObject;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector3 targetPosition;

    public void Activate()
    {
        if(targetObject)
        {
            agent.SetDestination(targetObject.transform.position);
        }
        else if(targetTransform)
        {
            agent.SetDestination(targetTransform.position);
        }
        else
        {
            //agent.SetDestination(targetPosition);
        }
    }

    public void SetGameObject(GameObject setTo)
    {
        targetObject = setTo;
    }

    public void SetGameObject(GameObject[] setTo)
    {
        targetObject = setTo[0];
    }
}
