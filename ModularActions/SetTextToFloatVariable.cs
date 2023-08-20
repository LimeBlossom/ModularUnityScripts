using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SetTextToFloatVariable : MonoBehaviour, IActivatable
{
    [SerializeField] private bool onUpdate;
    [SerializeField] private Text toChange;
    [SerializeField] private TextMeshProUGUI tmpToChange;
    [SerializeField] private FloatVariable value;
    [SerializeField] private string formatting;

    public void Activate()
    {
        if(toChange != null)
        {
            toChange.text = value.value.ToString(formatting);
        }
        if(tmpToChange != null)
        {
            tmpToChange.text = value.value.ToString(formatting);
        }
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
