using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAction : MonoBehaviour, IActivatable
{
    [SerializeField] private bool randomizeToSpawn = false;
    [SerializeField] private GameObjectReference[] toSpawn;
    [SerializeField] private Transform[] spawnLocation;
    [SerializeField] private Transform spawnRotation;
    [SerializeField] private GameObject parentTo;
    [SerializeField] private bool debug;

    public bool CanSpawn()
    {
        if(toSpawn.Length > 0)
        {
            if(toSpawn[0].value != null)
            { return true; }
        }
        return false;
    }

    public GameObject[] SpawnArray()
    {
        List<GameObject> spawnList = new();
        foreach(GameObjectReference goRef in toSpawn)
        {
            spawnList.Add(goRef.value);
        }
        return spawnList.ToArray();
    }

    public void Activate()
    {
        if (debug)
            print("SpawnAction activated");
        GameObject spawned = null;
        if(randomizeToSpawn)
        {
            spawned = Instantiate(toSpawn[Random.Range(0, toSpawn.Length)].value);
            AdjustSpawned(spawned);
        }
        else
        {
            foreach (GameObjectReference spawn in toSpawn)
            {
                if(spawnLocation.Length > 0)
                {
                    spawned = Instantiate(spawn.value, spawnLocation[Random.Range(0, spawnLocation.Length)].position, spawn.value.transform.rotation);
                }
                else
                {
                    spawned = Instantiate(spawn.value);
                }
                
                AdjustSpawned(spawned);
            }
        }
    }

    private void AdjustSpawned(GameObject spawned)
    {
        //if (spawnLocation.Length > 0)
        //{
        //    Transform tempLocation = spawnLocation[Random.Range(0, spawnLocation.Length)];
        //    if (spawned != null)
        //    {
        //        spawned.transform.position = tempLocation.position;
        //    }
        //}
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
