using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAction : MonoBehaviour, IActivatable
{
    [SerializeField] private Transform[] toRotate;
    [SerializeField] private Vector3 rotation;

    public void Activate()
    {
        foreach(Transform trans in toRotate)
        {
            trans.Rotate(rotation * Time.deltaTime);
        }
    }
}
