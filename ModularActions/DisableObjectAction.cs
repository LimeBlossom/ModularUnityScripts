using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObjectAction : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject[] toDisable;

    public void Activate()
    {
        foreach (GameObject go in toDisable)
        {
            go.SetActive(false);
        }
    }
}
