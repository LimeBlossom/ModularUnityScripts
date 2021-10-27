using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnClicked : MonoBehaviour
{
    [SerializeField] private GameObject[] toSpawn;
    [SerializeField] private bool spawnAtParentPosition = true;

    private void Start()
    {
        if(!GetComponent<Collider>() && !GetComponent<Collider2D>())
        {
            Debug.LogError(gameObject.name + " needs a collider to work with OnMouseDown.");
        }
    }

    private void OnMouseDown()
    {
        foreach (GameObject spawn in toSpawn)
        {
            GameObject spawned = Instantiate(spawn);
            if (spawnAtParentPosition)
            {
                spawned.transform.position = transform.position;
            }
        }
    }
}
