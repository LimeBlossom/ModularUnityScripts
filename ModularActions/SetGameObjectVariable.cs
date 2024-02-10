using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameObjectVariable : MonoBehaviour, IActivatable, ISettableGameObject
{
    [SerializeField] private GameObjectVariable toChange;
    [SerializeField] private GameObjectReference toChangeTo;

    [SerializeField] private bool debug;

    public void Activate()
    {
        if (debug)
        {
            Debug.Log(gameObject.name + " Activate()");
        }
        toChange.value = toChangeTo.value;
    }

    public void SetGameObject(GameObject setTo)
    {
        toChangeTo.constantValue = setTo;
        toChangeTo.useConstant = true;
    }

    public void SetGameObject(GameObject[] setTo)
    {
        SetGameObject(setTo[0]);
    }
}
