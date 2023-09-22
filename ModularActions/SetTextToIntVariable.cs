using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SetTextToIntVariable : MonoBehaviour, IActivatable
{
    [SerializeField] private bool onUpdate;
    [SerializeField] private Text toChange;
    [SerializeField] private TextMeshProUGUI tmpToChange;
    [SerializeField] private IntVariable value;

    public void Activate()
    {
        if (toChange != null)
        {
            toChange.text = value.value.ToString();
        }
        if (tmpToChange != null)
        {
            tmpToChange.text = value.value.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (onUpdate)
        {
            Activate();
        }
    }
}
