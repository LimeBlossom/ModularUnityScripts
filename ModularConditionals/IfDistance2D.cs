using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IfDistance2D : MonoBehaviour, IActivatable
{
    [SerializeField] private float isLargerThan = -1;
    [SerializeField] private float isSmallerThan = -1;

    [SerializeField] private bool onUpdate = true;

    [SerializeField] private GameObject objectA;
    [SerializeField] private GameObject objectB;

    [SerializeField] private string objectATag;
    [SerializeField] private string objectBTag;

    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    public void Activate()
    {
        CheckDistances();
    }

    private void Update()
    {
        if(onUpdate)
        {
            if (objectA == null)
            {
                objectA = GameObject.FindGameObjectWithTag(objectATag);
            }
            if (objectB == null)
            {
                objectB = GameObject.FindGameObjectWithTag(objectBTag);
            }
            if (objectA != null && objectB != null)
            {
                CheckDistances();
            }
        }
    }

    private void CheckDistances()
    {
        if(isLargerThan > -1)
        {
            if (DistanceIsLarger(objectA, objectB, isLargerThan))
            {
                ActivateActions();
            }
        }

        if (isSmallerThan > -1)
        {
            if (DistanceIsSmaller(objectA, objectB, isSmallerThan))
            {
                ActivateActions();
            }
        }
    }

    private bool DistanceIsLarger(GameObject objectA, GameObject objectB, float distance)
    {
        if(Vector2.Distance(objectA.transform.position, objectB.transform.position) > distance)
        {
            return true;
        }
        return false;
    }

    private bool DistanceIsSmaller(GameObject objectA, GameObject objectB, float distance)
    {
        if (Vector2.Distance(objectA.transform.position, objectB.transform.position) < distance)
        {
            return true;
        }
        return false;
    }

    private void ActivateActions()
    {
        events.Invoke();
        if (actions != null && actions.Length > 0)
        {
            foreach (IActivatable action in actions)
            {
                action.Activate();
            }
        }
    }
}
