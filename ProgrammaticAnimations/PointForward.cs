using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointForward : MonoBehaviour
{
    [SerializeField] private Transform toPoint;
    [SerializeField] private Rigidbody rb;

    void FixedUpdate()
    {
        toPoint.LookAt(toPoint.position + rb.velocity);
    }
}
