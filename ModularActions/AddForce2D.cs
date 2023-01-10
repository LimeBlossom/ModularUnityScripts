using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce2D : MonoBehaviour, IActivatable
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 minForceToAdd;
    [SerializeField] private Vector2 maxForceToAdd;
    [SerializeField] private MinMaxFloat forceScalar;
    [SerializeField] private ForceMode2D forceMode;
    [SerializeField] private bool relativeToForward = false;

    public void Activate()
    {
        if (relativeToForward)
        {
            rb.AddForce(rb.transform.right * Random.Range(minForceToAdd.x, maxForceToAdd.x) * Random.Range(forceScalar.min, forceScalar.max), forceMode);
            rb.AddForce(rb.transform.up * Random.Range(minForceToAdd.y, maxForceToAdd.y) * Random.Range(forceScalar.min, forceScalar.max), forceMode);
        }
        else
        {
            rb.AddForce(new Vector2(Random.Range(minForceToAdd.x, maxForceToAdd.x), Random.Range(minForceToAdd.y, maxForceToAdd.y)) * Random.Range(forceScalar.min, forceScalar.max), forceMode);
        }
    }
}
