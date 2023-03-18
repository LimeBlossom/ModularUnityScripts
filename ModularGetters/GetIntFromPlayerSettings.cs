using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetIntFromPlayerSettings : MonoBehaviour, IActivatable
{
    [SerializeField] private string intName;
    [SerializeField] private MonoBehaviour[] intSettables;

    [SerializeField] private bool debug = false;

    public void Activate()
    {
        int toPass = PlayerPrefs.GetInt(intName);
        foreach (MonoBehaviour behaviour in intSettables)
        {
            ISettableInt settable = behaviour as ISettableInt;
            if (settable != null)
            {
                if(debug)
                {
                    Debug.Log($"{gameObject.name} set int from player settings for {behaviour.gameObject.name}");
                }
                settable.SetInt(toPass);
            }
        }
    }
}
