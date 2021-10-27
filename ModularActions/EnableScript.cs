using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableScript : MonoBehaviour, IActivatable
{
    [SerializeField] private MonoBehaviour[] scripts;

    public void Activate()
    {
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = true;
        }
    }
}
