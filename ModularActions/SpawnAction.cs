using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAction : MonoBehaviour, IActivatable
{
    [SerializeField] private bool randomizeToSpawn = false;
    [SerializeField] private GameObject[] toSpawn;
    [SerializeField] private Transform[] spawnLocation;
    [SerializeField] private Transform spawnRotation;
    [SerializeField] private GameObject parentTo;

    public void Activate()
    {
        GameObject spawned = null;
        if(randomizeToSpawn)
        {
            spawned = Instantiate(toSpawn[Random.Range(0, toSpawn.Length)]);
        }
        else
        {
            foreach (GameObject spawn in toSpawn)
            {
                spawned = Instantiate(spawn);
            }
        }

        if (spawnLocation.Length > 0)
        {
            Transform tempLocation = spawnLocation[Random.Range(0, spawnLocation.Length)];
            if(spawned != null)
            {
                spawned.transform.position = tempLocation.position;
            } 
        }
        if (spawnRotation != null)
        {
            spawned.transform.rotation = spawnRotation.rotation;
        }
        if(parentTo != null)
        {
            spawned.transform.SetParent(parentTo.transform);
        }
    }
}
