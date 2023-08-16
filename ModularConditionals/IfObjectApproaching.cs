using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IfObjectApproaching : MonoBehaviour
{
    [SerializeField] private Transform targetCenter;
    [SerializeField] private float approachingThreshold = 0.2f; // Adjust this value to control the sensitivity of approach detection
    [SerializeField] private GameObject[] detectableObjects;

    [SerializeField] private UnityEvent events;

    private Rigidbody rb;

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject go in detectableObjects)
        {
            if (other.name == string.Format("{0}(Clone)", go.name))
            {
                CheckApproaching(other.attachedRigidbody);
            }
        }
    }

    private void CheckApproaching(Rigidbody rb)
    {
        if (targetCenter == null)
            targetCenter = transform;

        Vector3 directionToCenter = (targetCenter.position - rb.position).normalized;
        Vector3 velocityDirection = rb.velocity.normalized;
        float dotProduct = Vector3.Dot(velocityDirection, directionToCenter);

        //Debug.Log($"Checking if approaching, dotProduct: {dotProduct}");

        if (dotProduct >= approachingThreshold)
        {
            // Object is approaching the center
            events.Invoke();
        }
    }
}