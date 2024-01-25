using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IfDistance : MonoBehaviour, IActivatable, ISettableGameObject
{
    [SerializeField] private float isLargerThan = -1;
    [SerializeField] private float isSmallerThan = -1;

    [SerializeField] private bool onUpdate;

    [SerializeField] private GameObject fixedObject;
    [SerializeField] private GameObject settableObject;

    [SerializeField] private string fixedObjectTag;
    [SerializeField] private string settableObjectTag;

    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    [SerializeField] private bool debug;

    public void Activate()
    {
        if(debug)
        {
            Debug.Log(gameObject.name + " Activate()");
        }
        GetObjects();
        if (fixedObject != null && settableObject != null)
        {
            CheckDistances();
        }
    }

    public void SetGameObject(GameObject setTo)
    {
        settableObject = setTo;
    }

    private void Update()
    {
        if (onUpdate)
        {
            Activate();
        }
    }

    private void GetObjects()
    {
        if (debug)
        {
            Debug.Log(gameObject.name + " GetObjects()");
        }
        if (fixedObject == null)
        {
            if(fixedObjectTag != "")
            {
                fixedObject = GameObject.FindGameObjectWithTag(fixedObjectTag);
            }
        }
        if (settableObject == null)
        {
            if (settableObjectTag != "")
            {
                settableObject = GameObject.FindGameObjectWithTag(settableObjectTag);
            }
        }
    }

    private void CheckDistances()
    {
        if(debug)
        {
            Debug.Log(gameObject.name + " CheckDistances()");
        }
        if (isLargerThan > -1)
        {
            if (DistanceIsLarger(fixedObject, settableObject, isLargerThan))
            {
                ActivateActions();
            }
        }

        if (isSmallerThan > -1)
        {
            if (debug)
            {
                Debug.Log(gameObject.name + " CheckDistances() checking if smaller");
            }
            if (DistanceIsSmaller(fixedObject, settableObject, isSmallerThan))
            {
                if (debug)
                {
                    Debug.Log(gameObject.name + " CheckDistances() distance is smaller");
                }
                ActivateActions();
            }
        }
    }

    private bool DistanceIsLarger(GameObject objectA, GameObject objectB, float distance)
    {
        if (Vector3.Distance(objectA.transform.position, objectB.transform.position) > distance)
        {
            return true;
        }
        return false;
    }

    private bool DistanceIsSmaller(GameObject objectA, GameObject objectB, float distance)
    {
        if (Vector3.Distance(objectA.transform.position, objectB.transform.position) < distance)
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
