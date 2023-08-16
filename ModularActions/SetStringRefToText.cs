using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetStringRefToText : MonoBehaviour, IActivatable
{
    [SerializeField] private Text textObj;
    [SerializeField] private TextMeshProUGUI tmpObj;
    [SerializeField] private StringReference stringRef;

    public void Activate()
    {
        if(textObj != null)
        {
            stringRef.variable.value = textObj.text;
        }
        else if(tmpObj != null)
        {
            stringRef.variable.value = tmpObj.text;
        }
    }
}
