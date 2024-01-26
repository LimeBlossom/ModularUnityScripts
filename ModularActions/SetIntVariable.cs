using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIntVariable : MonoBehaviour, IActivatable, ISettableInt
{
    [SerializeField] private IntVariable toChange;
    [SerializeField] private IntReference toChangeTo;

    [SerializeField] private bool debug;

    public void Activate()
    {
        if (debug)
        {
            Debug.Log(gameObject.name + " Activate()" + toChangeTo.value);
        }
        toChange.SetValue(toChangeTo.value);
        if(debug)
            Debug.Log("New value: " + toChange.value);
    }

    public void SetInt(int setTo)
    {
        toChangeTo.constantValue = setTo;
        toChangeTo.useConstant = true;
    }

    public void SetInt(bool setTo)
    {
        toChangeTo.constantValue = setTo ? 1 : 0;
        toChangeTo.useConstant = true;
    }
}
