using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class IfRaycast2DObject : MonoBehaviour, IActivatable
{
    public GameObject nearestRaycastedTarget;

    [SerializeField] private Transform origin;
    [SerializeField] private GameObject[] toIgnore;

    [SerializeField] private List<GameObject> targets;
    [SerializeField] private string[] targetTags;

    [SerializeField] private bool onUpdate = false;

    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] gameObjectSettables;
    [SerializeField] private MonoBehaviour[] actions;

    [SerializeField] private bool debug;

    private void Start()
    {
        if(origin == null)
        {
            origin = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (onUpdate)
        {
            checkForHits();
        }
    }

    private void checkForHits()
    {
        float smallestDistance = Mathf.Infinity;
        GameObject nearestObj = null;

        foreach(GameObject target in targets)
        {
            if(raycastCanHit(target))
            {
                float dist = Vector3.Distance(origin.position, target.transform.position);
                if (dist < smallestDistance)
                {
                    smallestDistance = dist;
                    nearestObj = target;
                }
            }
        }
        
        foreach(string tag in targetTags)
        {
            foreach (GameObject target in GameObject.FindGameObjectsWithTag(tag))
            {
                if (raycastCanHit(target))
                {
                    float dist = Vector3.Distance(origin.position, target.transform.position);
                    if (dist < smallestDistance)
                    {
                        smallestDistance = dist;
                        nearestObj = target;
                    }
                }
            }
        }

        if(nearestObj != null)
        {
            ActivateActions(nearestObj);
        }
    }

    private bool raycastCanHit(GameObject target)
    {
        Vector3 pos = origin.position;
        Vector3 dir = (target.transform.position - pos).normalized;

        if (debug)
        {
            Debug.DrawLine(pos, pos + dir * 10, Color.red, Mathf.Infinity);
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(origin.position, dir * 300).OrderBy(h=>h.distance).ToArray();
        foreach(RaycastHit2D hit in hits)
        {
            if (debug)
            {
                Debug.Log(hit.collider.gameObject);
            }
            if (hit.collider != null)
            {
                if(!hit.collider.transform.IsChildOf(transform))
                {
                    bool skip = false;
                    foreach(GameObject ignoring in toIgnore)
                    {
                        if(hit.collider.transform.IsChildOf(ignoring.transform))
                        {
                            skip = true;
                        }
                    }
                    if(!skip)
                    {
                        if (hit.collider.gameObject == target)
                        {
                            return true;
                        }
                        else
                        {
                            if(debug)
                            {
                                Debug.Log(hit.collider.gameObject);
                            }
                            return false;
                        }
                    }
                }
            }
        }
        return false;
    }

    public void Activate()
    {
        checkForHits();
    }

    private void ActivateActions(GameObject nearestObject)
    {
        events.Invoke();
        // Call settables before actions
        if (gameObjectSettables != null && gameObjectSettables.Length > 0)
        {
            foreach (ISettableGameObject settable in gameObjectSettables)
            {
                settable.SetGameObject(nearestRaycastedTarget);
            }
        }
        if (actions != null && actions.Length > 0)
        {
            foreach (IActivatable action in actions)
            {
                action.Activate();
            }
        }
    }
}
