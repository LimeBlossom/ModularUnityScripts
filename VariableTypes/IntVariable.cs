using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewIntVar", menuName = "CustomSO/Types/IntVaraible")]
public class IntVariable : ScriptableObject
{
    public int value;
}

[Serializable]
public class IntReference
{
    public bool useConstant = true;
    public int constantValue;
    public IntVariable variable;

    public int value
    {
        get { return useConstant ? constantValue : variable.value; }
    }
}
