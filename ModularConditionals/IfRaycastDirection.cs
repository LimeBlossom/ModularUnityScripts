using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class IfRaycastDirection : MonoBehaviour, IActivatable
{
    [SerializeField] private Transform origin;
    [SerializeField] private List<string> targetTags;
    [SerializeField] private Direction[] directions;
    [SerializeField] private float maxDistance;
    [SerializeField] private bool onUpdate = false;

    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    [SerializeField] private bool ifNot = false;

    [SerializeField] private bool debug = false;

    void Start()
    {
        if (origin == null)
        {
            origin = transform;
        }
    }


    void Update()
    {
        if(onUpdate)
        {
            if(CheckForHits() == ifNot)
            {
                ActivateActions(null);
            }
        }
    }

    private bool CheckForHits()
    {
        float smallestDistance = Mathf.Infinity;
        GameObject nearestGO = null;

        foreach(Direction dir in directions)
        {
            RaycastHit[] hits = Physics.RaycastAll(origin.position, GetDirection(dir), maxDistance).OrderBy(h => h.distance).ToArray();
            foreach (RaycastHit hit in hits)
            {
                if (debug)
                {
                    print($"CheckForHits hit {hit.collider.name}, which has tag {hit.collider.tag}");
                }
                if (hit.collider.gameObject == origin.gameObject)
                {
                    continue;
                }
                if (targetTags.Contains(hit.collider.tag))
                {
                    float dist = Vector3.Distance(origin.position, hit.transform.position);
                    if (dist < smallestDistance)
                    {
                        smallestDistance = dist;
                        nearestGO = hit.collider.gameObject;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        if(nearestGO != null)
        {
            ActivateActions(nearestGO);
            return true;
        }
        else
        {
            return false;
        }
    }

    private Vector3 GetDirection(Direction direction)
    {
        Vector3 dir = Vector3.zero;
        switch(direction)
        {
            case Direction.Up:
                dir = origin.up;
                break;
            case Direction.Down:
                dir = -origin.up;
                break;
            case Direction.Right:
                dir = origin.right;
                break;
            case Direction.Left:
                dir = -origin.right;
                break;
            case Direction.Forward:
                dir = origin.forward;
                break;
            case Direction.Backward:
                dir = -origin.forward;
                break;
            default:
                break;
        }
        return dir;
    }

    public void Activate()
    {
        CheckForHits();
    }

    private void ActivateActions(GameObject nearestObject)
    {
        if(debug)
        {
            print($"{gameObject.name}'s IfRaycastDirection is Activating Actions");
        }
        events.Invoke();
        if (actions != null && actions.Length > 0)
        {
            foreach(MonoBehaviour behaviour in actions)
            {
                if(behaviour is ISettableGameObject)
                {
                    (behaviour as ISettableGameObject).SetGameObject(nearestObject);
                }
            }

            foreach (IActivatable action in actions)
            {
                action.Activate();
            }
        }
    }
}
