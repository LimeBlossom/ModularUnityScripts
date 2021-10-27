using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToFloatVariable : MonoBehaviour, IActivatable
{
    [SerializeField] private FloatVariable var;
    [SerializeField] private float amount;

    [SerializeField] private bool debug;

    public void Activate()
    {
        if(debug)
        {
            Debug.Log(gameObject.name + " Activate()");
        }
        var.value += amount;
    }
}
