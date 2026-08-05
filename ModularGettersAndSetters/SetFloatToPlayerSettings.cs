using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFloatToPlayerSettings : MonoBehaviour, IActivatable
{
    [SerializeField] private string floatName;
    [SerializeField] private FloatVariable floatVariable;

    [SerializeField] private bool debug = false;

    public void Activate()
    {
        PlayerPrefs.SetFloat(floatName, floatVariable.value);
    }
}
