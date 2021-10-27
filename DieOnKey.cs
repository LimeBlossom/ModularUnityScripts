using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnKey : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnOnDeath;
    [SerializeField] private string[] dieKey;
    [SerializeField] private GameObject spawnPosition;

    // Update is called once per frame
    void Update()
    {
        foreach (string input in dieKey)
        {
            if (Input.GetKeyDown(input))
            {
                // Spawn
                foreach(GameObject toSpawn in spawnOnDeath)
                {
                    GameObject spawned = Instantiate(toSpawn);
                    spawned.transform.rotation = transform.rotation;
                    spawned.transform.localScale = new Vector3(spawned.transform.localScale.x * (transform.localScale.x > 0 ? 1 : -1), spawned.transform.localScale.y, spawned.transform.localScale.z);
                    if (spawnPosition)
                    {
                        spawned.transform.position = spawnPosition.transform.position;
                    }
                }
                // Die
                Destroy(gameObject);
            }
        }
    }
}
