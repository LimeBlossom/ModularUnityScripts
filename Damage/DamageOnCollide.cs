using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageOnCollide : MonoBehaviour
{
    public damageTypes damageType;
    public float radius;
    public float radiusOffset;
    public float velocityRequired;
    [SerializeField] private float curVelocity;
    [SerializeField] private float lastFrameVelocity;

    [SerializeField] private bool onTrigger = false;

    [SerializeField] private bool selfDestructOnCollide = false;
    [SerializeField] private bool selfDestructOnBreak = false;

    [SerializeField] private UnityEvent onDestroyEvents;
    [SerializeField] private MonoBehaviour[] onDestroyActions;

    [SerializeField] private bool debug;

    private float timeActive = 0;
    [SerializeField] private bool isActive = true;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        curVelocity = rb.velocity.magnitude;
        // If we're moving fast enough
        if (curVelocity > velocityRequired || timeActive < 0.01f)
        {
            int layer = 1 << LayerMask.NameToLayer("Default");
            RaycastHit[] hits = Physics.RaycastAll(
                transform.position,
                rb.velocity);

            foreach (RaycastHit hit in hits)
            {
                if(hit.collider.isTrigger)
                {
                    continue;
                }
                if (hit.distance < radius + radiusOffset)
                {
                    if (debug)
                    {
                        Debug.Log($"isActive:{isActive}, timeActive:{timeActive}, velocity:{rb.velocity.magnitude}");
                    }
                    // If we've hit something breakable
                    BreakHit(hit.transform, true);
                }
            }
        }
        timeActive += Time.deltaTime;
        lastFrameVelocity = rb.velocity.magnitude;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(debug)
        {
            Debug.Log($"isActive:{isActive}, timeActive:{timeActive}, velocity:{rb.velocity.magnitude}");
        }
        if (isActive)
        {
            // If we're moving fast enough
            if (rb.velocity.magnitude > velocityRequired || lastFrameVelocity > velocityRequired || timeActive < 0.01f)
            {
                if(debug)
                {
                    Debug.Log($"Calling BreakHit on {collision.gameObject.name}");
                }
                // If we've hit something breakable
                BreakHit(collision.transform);
            }
            else
            {
                rb.useGravity = true;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (debug)
        {
            Debug.Log($"isActive:{isActive}, timeActive:{timeActive}, velocity:{rb.velocity.magnitude}");
        }
        if (isActive && onTrigger)
        {
            // If we're moving fast enough
            if (rb.velocity.magnitude > velocityRequired || lastFrameVelocity > velocityRequired || timeActive < 0.01f)
            {
                // If we've hit something breakable
                BreakHit(collision.transform);
            }
        }
    }

    private void BreakHit(Transform t, bool fromRaycast = false)
    {
        // If we've hit something breakable
        foreach (IBreakable breakable in t.GetComponents<IBreakable>())
        {
            if (breakable.CanBreakFrom(damageType))
            {
                if (debug)
                {
                    Debug.Log($"Calling Break on {t.name}");
                }
                bool broke = breakable.Break(damageType);
                if (broke && selfDestructOnBreak)
                {
                    if (debug)
                    {
                        Debug.Log($"Collided with {t.name}. Self destructing.");
                    }
                    if(onDestroyEvents.GetPersistentEventCount() > 0 || onDestroyActions.Length > 0)
                    {
                        ActivateActions();
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
                else if (!broke && selfDestructOnCollide)
                {
                    if (debug)
                    {
                        Debug.Log($"Collided with {t.name}. Self destructing.");
                    }
                    if (onDestroyEvents.GetPersistentEventCount() > 0 || onDestroyActions.Length > 0)
                    {
                        ActivateActions();
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
                return;
            }
        }
        if (!fromRaycast && selfDestructOnCollide && !t.GetComponentInChildren<Drum>())
        {
            if (debug)
            {
                Debug.Log($"Collided with {t.name}. Self destructing.");
            }
            if (onDestroyEvents.GetPersistentEventCount() > 0 || onDestroyActions.Length > 0)
            {
                ActivateActions();
            }
            else
            {
                Destroy(gameObject);
            }
            return;
        }
    }

    private void ActivateActions()
    {
        onDestroyEvents.Invoke();
        if (onDestroyActions != null && onDestroyActions.Length > 0)
        {
            foreach (IActivatable action in onDestroyActions)
            {
                action.Activate();
            }
        }
    }
}
