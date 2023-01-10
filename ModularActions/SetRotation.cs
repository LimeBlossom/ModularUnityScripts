using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRotation : MonoBehaviour, IActivatable
{
    [SerializeField] private Vector3 rotation;
    [SerializeField] private bool global;

    public void Activate()
    {
        if(global)
        {
            transform.eulerAngles = rotation;
        }
        else
        {
            transform.localEulerAngles = rotation;
        }
    }
}
