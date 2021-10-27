using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] bool trackX;
    [SerializeField] bool trackY;
    [SerializeField] bool trackZ;
    [SerializeField] bool initializeX;
    [SerializeField] bool initializeY;
    [SerializeField] bool initializeZ;

    private void Start()
    {
        if(initializeX)
        {
            Camera.main.transform.position = new Vector3(transform.position.x + offset.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
        if (initializeY)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y + offset.y, Camera.main.transform.position.z);
        }
        if (initializeZ)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z + offset.z);
        }
    }

    void Update()
    {
        if(trackX)
        {
            Camera.main.transform.position = new Vector3(transform.position.x + offset.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
        if (trackY)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y + offset.y, Camera.main.transform.position.z);
        }
        if (trackZ)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z + offset.z);
        }
    }
}
