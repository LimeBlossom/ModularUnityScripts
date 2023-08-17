using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnCollision : MonoBehaviour
{
    public int healthPoints = 1;
    public string[] objectsThatHurt;
    public GameObject[] spawnOnDeath;
    public bool spawnAtPosition;

    public bool debug = false;

    private void Start()
    {
        if (!GetComponent<Collider2D>())
        {
            Debug.LogError(gameObject.name + " checks for collision but does not have a collider!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (debug) { Debug.Log(collision.gameObject.name); }
        for (int i = 0; i < objectsThatHurt.Length; i++)
        {
            if (collision.gameObject.name.Contains(objectsThatHurt[i]))
            {
                healthPoints--;
                if(healthPoints <= 0)
                {
                    //DIE
                    if (spawnOnDeath.Length > 0)
                    {
                        foreach (GameObject toSpawn in spawnOnDeath)
                        {
                            GameObject spawned = Instantiate(toSpawn);
                            if (spawnAtPosition)
                            {
                                spawned.transform.position = transform.position;
                            }
                        }
                    }
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (debug) { Debug.Log(collision.gameObject.name); }
        for (int i = 0; i < objectsThatHurt.Length; i++)
        {
            if (collision.gameObject.name.Contains(objectsThatHurt[i]))
            {
                healthPoints--;
                if (healthPoints <= 0)
                {
                    //DIE
                    if (spawnOnDeath.Length > 0)
                    {
                        foreach (GameObject toSpawn in spawnOnDeath)
                        {
                            GameObject spawned = Instantiate(toSpawn);
                            if (spawnAtPosition)
                            {
                                spawned.transform.position = transform.position;
                            }
                        }
                    }
                    Destroy(gameObject);
                }
            }
        }
    }
}
