using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVelocityOnCollision : MonoBehaviour
{
    [SerializeField] private string[] objectsThatMoveUs;
    [SerializeField] private Vector2 newVelocity;
    [SerializeField] private bool relativeToOwnFacing;
    [SerializeField] private bool relativeToCollidedFacing;
    [SerializeField] private GameObject[] toSpawn;

    [SerializeField] private bool debug;

    private Rigidbody2D rb;

    private void Start()
    {
        if (!GetComponent<Collider2D>())
        {
            Debug.LogError(gameObject.name + " checks for collision but does not have a collider!");
        }
        if (!GetComponent<Rigidbody2D>())
        {
            Debug.LogError(gameObject.name + " sets velocity but does not have a rigidbody!");
        }
        else
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (debug) { Debug.Log(collision.gameObject.name); }
        foreach(string value in objectsThatMoveUs)
        {
            if (collision.gameObject.name.Contains(value))
            {
                SetVelocity(newVelocity, collision.gameObject);
                Spawn();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (debug) { Debug.Log(collision.gameObject.name); }
        foreach (string value in objectsThatMoveUs)
        {
            if (collision.gameObject.name.Contains(value))
            {
                SetVelocity(newVelocity, collision.gameObject);
                Spawn();
            }
        }
    }

    private void SetVelocity(Vector2 value, GameObject collided)
    {
        if(relativeToOwnFacing)
        {
            rb.velocity = new Vector2(value.x * (transform.right.x * transform.lossyScale.x > 0 ? -1 : 1), value.y);
        }
        else if(relativeToCollidedFacing)
        {
            rb.velocity = new Vector2(value.x * (collided.transform.right.x * collided.transform.lossyScale.x > 0 ? -1 : 1), value.y);
        }
        else
        {
            rb.velocity = value;
        }
    }

    private void Spawn()
    {
        foreach (GameObject spawn in toSpawn)
        {
            GameObject spawned = Instantiate(spawn);
            spawned.transform.rotation = transform.rotation;
            spawned.transform.localScale = new Vector3(spawned.transform.localScale.x * (transform.localScale.x > 0 ? 1 : -1), spawned.transform.localScale.y, spawned.transform.localScale.z);
        }
    }
}
