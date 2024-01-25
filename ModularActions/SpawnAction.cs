using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAction : MonoBehaviour, IActivatable
{
    [SerializeField] private bool randomizeToSpawn = false;
    [SerializeField] private GameObject[] toSpawn;
    [SerializeField] private GameObjectReference[] refToSpawn;
    [SerializeField] private Transform[] spawnLocation;
    [SerializeField] private Transform spawnRotation;
    [SerializeField] private GameObject parentTo;
    [SerializeField] private bool debug;

    public void Activate()
    {
        GameObject spawned = null;
        if(randomizeToSpawn)
        {
            if(toSpawn.Length > 0)
            {
                spawned = Instantiate(toSpawn[Random.Range(0, toSpawn.Length)]);
                AdjustSpawned(spawned);
            }
            if (refToSpawn.Length > 0)
            {
                spawned = Instantiate(refToSpawn[Random.Range(0, refToSpawn.Length)].value);
                AdjustSpawned(spawned);
            }
        }
        else
        {
            foreach (GameObject spawn in toSpawn)
            {
                spawned = Instantiate(spawn);
                AdjustSpawned(spawned);
            }
            foreach (GameObjectReference spawn in refToSpawn)
            {
                if(spawn.value != null)
                {
                    spawned = Instantiate(spawn.value);
                    AdjustSpawned(spawned);
                }
            }
        }
    }

    private void AdjustSpawned(GameObject spawned)
    {
        if (spawnLocation.Length > 0)
        {
            Transform tempLocation = spawnLocation[Random.Range(0, spawnLocation.Length)];
            if (spawned != null)
            {
                spawned.transform.position = tempLocation.position;
            }
        }
        if (spawnRotation != null)
        {
            spawned.transform.rotation = spawnRotation.rotation;
        }
        if (parentTo != null)
        {
            spawned.transform.SetParent(parentTo.transform);
        }
    }
}
