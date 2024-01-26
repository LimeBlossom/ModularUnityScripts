using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIntToPlayerSettings : MonoBehaviour, IActivatable
{
    [SerializeField] private string intName;
    [SerializeField] private IntVariable intVariable;

    [SerializeField] private bool debug = false;

    public void Activate()
    {
        JSONPlayerPrefs.Instance.SetInt(intName, intVariable.value);
    }
}
