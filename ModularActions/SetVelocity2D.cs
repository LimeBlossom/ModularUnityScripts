using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVelocity2D : MonoBehaviour, IActivatable
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector3 minVelocity;
    [SerializeField] private Vector3 maxVelocity;

    public void Activate()
    {
        rb.velocity = new Vector3(
            Random.Range(minVelocity.x, maxVelocity.x),
            Random.Range(minVelocity.y, maxVelocity.y),
            Random.Range(minVelocity.z, maxVelocity.z));
    }
}
