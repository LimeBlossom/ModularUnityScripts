using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFloatVar", menuName = "CustomSO/Types/FloatVariable")]
public class FloatVariable : ScriptableObject
{
    public float value;
}

[System.Serializable]
public class FloatReference
{
    public bool useConstant = true;
    public float constantValue;
    public FloatVariable variable;

    public float value
    {
        get { return useConstant ? constantValue : variable.value; }
    }
}