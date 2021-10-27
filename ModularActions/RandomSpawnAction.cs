using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnAction : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject[] toSpawn;
    [SerializeField] private Transform atPosition;

    public void Activate()
    {
        GameObject spawned = Instantiate(toSpawn[Random.Range(0, toSpawn.Length)]);
        if(atPosition != null)
        {
            spawned.transform.position = atPosition.position;
        }
    }
}
