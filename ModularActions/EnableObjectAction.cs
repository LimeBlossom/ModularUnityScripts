using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectAction : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject[] toEnable;

    public void Activate()
    {
        foreach(GameObject go in toEnable)
        {
            go.SetActive(true);
        }
    }
}
