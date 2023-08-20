using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class IfRaycastObject : MonoBehaviour, IActivatable, IGettableGameObject
{
    public GameObject nearestRaycastedTarget;

    [SerializeField] private Transform origin;
    [SerializeField] private GameObject[] toIgnore;

    [SerializeField] private List<GameObject> targets;
    [SerializeField] private string[] targetTags;

    [SerializeField] private bool onUpdate = false;

    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    [SerializeField] private bool debug;

    public void Activate()
    {
        CheckForHits();
    }

    public GameObject GetGameObject()
    {
        return nearestRaycastedTarget;
    }

    private void Start()
    {
        if (origin == null)
        {
            origin = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (onUpdate)
        {
            CheckForHits();
        }
    }

    private void CheckForHits()
    {
        float smallestDistance = Mathf.Infinity;
        GameObject nearestObj = null;

        foreach (GameObject target in targets)
        {
            if (RaycastCanHit(target, origin, toIgnore, targetTags))
            {
                float dist = Vector3.Distance(origin.position, target.transform.position);
                if (dist < smallestDistance)
                {
                    smallestDistance = dist;
                    nearestObj = target;
                }
            }
        }

        foreach (string tag in targetTags)
        {
            foreach (GameObject target in GameObject.FindGameObjectsWithTag(tag))
            {
                if (RaycastCanHit(target, origin, toIgnore, targetTags))
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

        if (nearestObj != null)
        {
            nearestRaycastedTarget = nearestObj;
            ActivateActions(nearestObj);
        }
    }

    static public bool RaycastCanHit(GameObject target, Transform origin, GameObject[] toIgnore = null, string[] targetTags = null)
    {
        Vector3 pos = origin.position;
        Vector3 dir = (target.transform.position - pos).normalized;

        //if (debug)
        //{
        //    Debug.DrawLine(pos, pos + dir * 10, Color.red, Mathf.Infinity);
        //}


        RaycastHit[] hits = Physics.RaycastAll(origin.position, dir * 300).OrderBy(h => h.distance).ToArray();
        //if(debug)
        //{
        //    foreach(RaycastHit hit in hits)
        //    {
        //        Debug.Log(hit.collider.gameObject.name);
        //    }
        //}
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider != null)
            {
                if (!hit.collider.transform.IsChildOf(origin))
                {
                    bool skip = false;
                    if(toIgnore != null)
                    {
                        foreach (GameObject ignoring in toIgnore)
                        {
                            if (hit.collider.transform.IsChildOf(ignoring.transform))
                            {
                                skip = true;
                            }
                        }
                    }
                    if (!skip)
                    {
                        if (hit.collider.gameObject == target || (targetTags != null && targetTags.Contains(hit.collider.tag)))
                        {
                            return true;
                        }
                        else
                        {
                            //if (debug)
                            //{
                            //    Debug.Log(hit.collider.gameObject);
                            //}
                            return false;
                        }
                    }
                }
            }
        }
        return false;
    }

    private void ActivateActions(GameObject nearestObject)
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
