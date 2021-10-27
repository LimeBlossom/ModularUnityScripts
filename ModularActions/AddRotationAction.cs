using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRotationAction : MonoBehaviour, IActivatable
{
    [SerializeField] private Transform toRotate;

    [SerializeField] private Vector3 rotateAmount;

    private void Start()
    {
        if(toRotate == null)
        {
            toRotate = transform;
        }
    }

    public void Activate()
    {
        toRotate.transform.eulerAngles = toRotate.transform.rotation.eulerAngles + rotateAmount;
    }
}
