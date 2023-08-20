using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVelocity : MonoBehaviour, IActivatable
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 minVelocity;
    [SerializeField] private Vector3 maxVelocity;
    [SerializeField] private float forwardVelocity;

    public void Activate()
    {
        rb.velocity = new Vector3(
            Random.Range(minVelocity.x, maxVelocity.x),
            Random.Range(minVelocity.y, maxVelocity.y),
            Random.Range(minVelocity.z, maxVelocity.z));
        if(forwardVelocity != 0)
        {
            rb.velocity = transform.forward * forwardVelocity;
        }
    }
}
