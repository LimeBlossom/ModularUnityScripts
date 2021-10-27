using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAction : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject[] toSpawn;
    [SerializeField] private Transform[] spawnLocation;
    [SerializeField] private Transform spawnRotation;

    public void Activate()
    {
        foreach (GameObject spawn in toSpawn)
        {
            GameObject spawned = Instantiate(spawn);
            if (spawnLocation.Length > 0)
            {
                Transform tempLocation = spawnLocation[Random.Range(0, spawnLocation.Length)];
                spawned.transform.position = tempLocation.position;
            }
            if(spawnRotation != null)
            {
                spawned.transform.rotation = spawnRotation.rotation;
            }
        }
    }
}
