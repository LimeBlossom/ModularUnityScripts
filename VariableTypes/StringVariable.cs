using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStringVar", menuName = "CustomSO/Types/StringVaraible")]
public class StringVariable : ScriptableObject
{
    public string value;
}

[System.Serializable]
public class StringReference
{
    public bool useConstant = true;
    public string constantValue;
    public StringVariable variable;

    public string value
    {
        get { return useConstant ? constantValue : variable.value; }
    }
}
