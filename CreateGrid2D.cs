using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid2D : MonoBehaviour
{
    [SerializeField] private GameObject toSpawn;
    [SerializeField] private int numWidth;
    [SerializeField] private int numHeight;
    [SerializeField] private Vector2 scalar;
    [SerializeField] private Vector3 startingPosition; // Lower left corner
    [SerializeField] private Transform parentTo;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numWidth; i++)
        {
            for(int j = 0; j < numHeight; j++)
            {
                GameObject spawned = Instantiate(toSpawn, new Vector3(startingPosition.x + i * scalar.x, startingPosition.y + j * scalar.y, startingPosition.z), Quaternion.identity);
                if(parentTo)
                {
                    spawned.transform.SetParent(parentTo);
                }
            }
        }
    }
}
