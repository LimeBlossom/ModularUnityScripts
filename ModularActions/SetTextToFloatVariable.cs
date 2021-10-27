using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SetTextToFloatVariable : MonoBehaviour, IActivatable
{
    [SerializeField] private bool onUpdate;
    [SerializeField] private Text toChange;
    [SerializeField] private FloatVariable value;

    public void Activate()
    {
        toChange.text = value.value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(onUpdate)
        {
            Activate();
        }
    }
}
