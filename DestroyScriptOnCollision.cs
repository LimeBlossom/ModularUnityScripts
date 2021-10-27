using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScriptOnCollision : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] scripts;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach(MonoBehaviour script in scripts)
        {
            Destroy(script);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (MonoBehaviour script in scripts)
        {
            Destroy(script);
        }
    }
}
