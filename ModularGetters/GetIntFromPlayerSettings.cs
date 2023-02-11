using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetIntFromPlayerSettings : MonoBehaviour, IActivatable
{
    [SerializeField] private string intName;
    [SerializeField] private MonoBehaviour[] intSettables;

    //[SerializeField] private bool debug = false;

    public void Activate()
    {
        int toPass = PlayerPrefs.GetInt(intName);
        foreach (MonoBehaviour behaviour in intSettables)
        {
            var settable = behaviour as ISettableInt;
            if (settable != null)
            {
                settable.SetInt(toPass);
            }
        }
    }
}
