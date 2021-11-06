using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAction : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject toDie;

    public void Activate()
    {
        if (toDie != null)
        {
            Destroy(toDie);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
