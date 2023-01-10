using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameObjectActive : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject toSet;
    [SerializeField] private bool active;

    public void Activate()
    {
        toSet.SetActive(active);
    }
}
