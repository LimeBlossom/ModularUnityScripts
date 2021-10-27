using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAsChildOnCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.parent = collision.transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.parent = collision.transform;
    }
}
