using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyScaleAction : MonoBehaviour, IActivatable
{
    [SerializeField] private Transform toScale;
    [SerializeField] private float multiplier;

    public void Activate()
    {
        toScale.localScale *= multiplier;
    }
}
