using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour, IActivatable
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 forceToAdd;
    [SerializeField] private float forceScalar = 1;
    [SerializeField] private ForceMode forceMode;
    [SerializeField] private bool relativeToForward = false;

    public void Activate()
    {
        Debug.Log("Activated");
        if(relativeToForward)
        {
            rb.AddForce(rb.transform.right * forceToAdd.x * forceScalar, forceMode);
            rb.AddForce(rb.transform.up * forceToAdd.y * forceScalar, forceMode);
            rb.AddForce(rb.transform.forward * forceToAdd.z * forceScalar, forceMode);
        }
        else
        {
            rb.AddForce(forceToAdd * forceScalar, forceMode);
        }
    }
}
