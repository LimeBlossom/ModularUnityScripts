using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIntVariable : MonoBehaviour, IActivatable
{
    [SerializeField] private IntVariable toChange;
    [SerializeField] private IntReference toChangeTo;

    [SerializeField] private bool debug;

    public void Activate()
    {
        if (debug)
        {
            Debug.Log(gameObject.name + " Activate()");
        }
        toChange.value = toChangeTo.value;
    }
}
