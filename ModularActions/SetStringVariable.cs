using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStringVariable : MonoBehaviour, IActivatable
{
    [SerializeField] private StringVariable toChange;
    [SerializeField] private StringReference toChangeTo;

    [SerializeField] private bool debug;

    public void Activate()
    {
        if (debug)
        {
            Debug.Log(gameObject.name + " Activate()");
        }
        toChange.value = toChangeTo.value;
    }

    public void SetString(string setTo)
    {
        toChangeTo.constantValue = setTo;
        toChangeTo.useConstant = true;
    }
}
