using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTimeScale : MonoBehaviour, IActivatable
{
    [SerializeField] private FloatReference timeScale;

    public void Activate()
    {
        Time.timeScale = timeScale.value;
    }
}
