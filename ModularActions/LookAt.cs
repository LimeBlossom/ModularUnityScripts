using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour, IActivatable
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform looker;

    public void Activate()
    {
        if(looker == null)
        {
            looker = transform;
        }

        looker.LookAt(target);
    }
}
