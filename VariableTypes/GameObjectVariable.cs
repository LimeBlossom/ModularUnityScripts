using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewGameObjectVar", menuName = "CustomSO/Types/GameObjectVariable")]
public class GameObjectVariable : ScriptableObject
{
    public GameObject value;
}

[Serializable]
public class GameObjectReference
{
    public bool useConstant = true;
    public GameObject constantValue;
    public GameObjectVariable variable;

    public GameObject value
    {
        get { return useConstant ? constantValue : variable.value; }
    }
}
