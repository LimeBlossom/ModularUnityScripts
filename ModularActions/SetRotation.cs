using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRotation : MonoBehaviour, IActivatable
{
    [SerializeField] private Transform toRotate;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private bool global;

    public void Activate()
    {
        if(global)
        {
            toRotate.eulerAngles = rotation;
        }
        else
        {
            toRotate.localEulerAngles = rotation;
        }
    }
}
