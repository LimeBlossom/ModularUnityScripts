using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFloatVariable : MonoBehaviour, IActivatable
{
    [SerializeField] private FloatVariable toChange;
    [SerializeField] private FloatReference toChangeTo;

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
