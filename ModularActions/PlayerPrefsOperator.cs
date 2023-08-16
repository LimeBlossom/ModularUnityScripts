using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsOperator : MonoBehaviour
{
    [SerializeField] private StringReference keyReference;
    [SerializeField] private StringReference valueReference;


    public void SaveStringToPlayerPrefs()
    {
        SaveStringToPlayerPrefs(keyReference.value, valueReference.value);
    }

    public void SaveStringToPlayerPrefs(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }
}
