using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAction : MonoBehaviour, IActivatable
{
    public void Activate()
    {
        Destroy(gameObject);
    }
}
