using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardAction : MonoBehaviour, IActivatable
{
    [SerializeField] private Transform[] toMove;
    [SerializeField] private float speed;

    public void Activate()
    {
        foreach(Transform trans in toMove)
        {
            trans.position += trans.forward * speed * Time.deltaTime;
        }
    }
}
