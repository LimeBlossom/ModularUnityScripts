using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class IfRaycastDirection : MonoBehaviour, IActivatable
{
    [SerializeField] private Transform origin;
    [SerializeField] private List<string> targetTags;
    [SerializeField] private List<string> ignoreTags;
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

    public GameObject CheckForHits(bool activateActions = true, Direction[] checkDirs = null, string[] tags = null, string[] ignore = null, bool inverseTag = false)
    {
        if(tags == null)
        {
            tags = targetTags.ToArray();
        }
        if(ignore == null)
        {
            ignore = ignoreTags.ToArray();
        }
        if(checkDirs == null)
        {
            checkDirs = directions;
        }
        float smallestDistance = Mathf.Infinity;
        GameObject nearestGO = null;

        foreach(Direction dir in checkDirs)
        {
            var hits = GetHits(dir);
            if (hits == null)
                continue;
            foreach (RaycastHit hit in hits)
            {
                if (debug)
                {
                    Debug.Log($"{transform.position.y}: CheckForHits hit {hit.collider.name}, which has tag {hit.collider.tag}");
                }
                if (hit.collider.gameObject == origin.gameObject || ignore.Contains(hit.collider.tag))
                {
                    continue;
                }
                if (!tags.Contains(hit.collider.tag) && !inverseTag)
                {
                    break;
                }
                else if(tags.Contains(hit.collider.tag) && inverseTag)
                {
                    break;
                }

                float dist = Vector3.Distance(origin.position, hit.transform.position);
                if (dist < smallestDistance)
                {
                    // Calculate the direction from the origin to the hit point
                    Vector2 hitDirection = new Vector2(hit.transform.position.x, hit.transform.position.z) -
                        new Vector2(origin.position.x, origin.position.z);
                    // Normalize the hit direction vector
                    hitDirection.Normalize();
                    // Calculate the dot product between the hit direction and the raycast direction
                    float dotProduct = Vector2.Dot(new Vector2(GetDirection(dir).x, GetDirection(dir).z), hitDirection);
                    // Check if the hit occurred in the same direction as the raycast
                    if (dotProduct > 0.99)
                    {
                        smallestDistance = dist;
                        nearestGO = hit.collider.gameObject;
                    }
                }
            }
        }

        if(nearestGO != null)
        {
            if(activateActions)
            {
                ActivateActions(nearestGO);
            }

            return nearestGO;
        }
        else
        {
            return null;
        }
    }

    public RaycastHit[] GetHits(Direction dir)
    {
        if(origin == null)
        {
            return null;
        }
        List<RaycastHit> hits = new();
        hits.AddRange(Physics.RaycastAll(origin.position, GetDirection(dir), maxDistance));
        hits.AddRange(Physics.RaycastAll(origin.position + origin.right * .25f, GetDirection(dir), maxDistance));
        hits.AddRange(Physics.RaycastAll(origin.position + origin.right * -.25f, GetDirection(dir), maxDistance));

        hits = hits.OrderBy(h => h.distance).ToList();
        return hits.ToArray();
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
