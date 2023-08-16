using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundAxis : MonoBehaviour, IActivatable
{
    [SerializeField] private Transform toRotate;

    [SerializeField] private Vector3 rotateSpeeds;



    private void Start()
    {
        if (toRotate == null)
        {
            toRotate = transform;
        }
    }

    public void Activate()
    {
        toRotate.transform.Rotate(Vector3.right, rotateSpeeds.x * Time.deltaTime);
        toRotate.transform.Rotate(Vector3.up, rotateSpeeds.y * Time.deltaTime);
        toRotate.transform.Rotate(Vector3.forward, rotateSpeeds.z * Time.deltaTime);
    }
}
