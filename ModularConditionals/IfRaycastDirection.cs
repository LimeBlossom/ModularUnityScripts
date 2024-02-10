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
    [SerializeField] private float width;
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
            if(CheckForHits() == false && ifNot)
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
            int EVERYTHINGLAYERMASK = ~0;

            if(origin == null)
            {
                return false;
            }
            //RaycastHit[] hits = Physics.RaycastAll(origin.position, VariableTypes.GetDirection(origin, dir), maxDistance, EVERYTHINGLAYERMASK, QueryTriggerInteraction.Ignore).OrderBy(h => h.distance).ToArray();
            RaycastHit[] hits = Physics.BoxCastAll(
                origin.position,
                Vector3.one * width,
                VariableTypes.GetDirection(origin, dir),
                Quaternion.identity,
                maxDistance,
                EVERYTHINGLAYERMASK,
                QueryTriggerInteraction.Ignore).OrderBy(h => h.distance).ToArray();
            if(debug)
                Debug.DrawRay(origin.position, VariableTypes.GetDirection(origin, dir) * maxDistance, Color.red);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject == origin.gameObject)
                {
                    continue;
                }
                if (targetTags.Contains(hit.collider.tag))
                {
                    if (debug)
                    {
                        print($"CheckForHits hit {hit.collider.name}, which has tag {hit.collider.tag}");
                    }

                    // Sanity Check
                    /* You would not believe why we have to do this. Insane bug!
                     * Somehow the raycast is finding hits that are not in the direction that it is pointing! */
                    Vector3 directionToHit = (hit.transform.position - origin.position).normalized;
                    Vector3 rayDirection = VariableTypes.GetDirection(origin, dir).normalized;
                    
                    if(Vector3.Dot(rayDirection, directionToHit) < 1)
                    {
                        continue;
                    }
                    if(Vector3.Distance(origin.position, hit.transform.position) > maxDistance)
                    {
                        continue;
                    }
                    // End Sanity Check


                    float dist = Vector3.Distance(origin.position, hit.transform.position);
                    if (dist < smallestDistance)
                    {
                        smallestDistance = dist;
                        nearestGO = hit.collider.gameObject;
                    }
                }
                else // If vision is blocked, don't turn
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
