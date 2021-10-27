using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToIntVariable : MonoBehaviour, IActivatable
{
    [SerializeField] private IntVariable var;
    [SerializeField] private int amount;

    [SerializeField] private bool debug;

    public void Activate()
    {
        if (debug)
        {
            Debug.Log(gameObject.name + " Activate()");
        }
        var.value += amount;
    }
}
