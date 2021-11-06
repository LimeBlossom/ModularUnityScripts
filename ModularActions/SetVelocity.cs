using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVelocity : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector3 newVelocity;

    public void Activate()
    {
        rb.velocity = newVelocity;
    }
}
