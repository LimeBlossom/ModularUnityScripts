using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFloatFromPlayerSettings : MonoBehaviour, IActivatable
{
    [SerializeField] private string floatName;
    [SerializeField] private MonoBehaviour[] floatSettables;

    [SerializeField] private bool debug = false;

    public void Activate()
    {
        float toPass = PlayerPrefs.GetFloat(floatName);
        foreach (MonoBehaviour behaviour in floatSettables)
        {
            ISettableFloat settable = behaviour as ISettableFloat;
            if (settable != null)
            {
                if(debug)
                {
                    Debug.Log($"{gameObject.name} set float from player settings for {behaviour.gameObject.name}");
                }
                settable.SetFloat(toPass);
            }
        }
    }
}
