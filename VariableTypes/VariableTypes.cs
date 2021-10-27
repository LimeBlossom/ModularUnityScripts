using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public struct MinMaxFloat { public float min; public float max; }
public struct MinMaxInt { public int min; public int max; }
public enum CardinalDirection { North, East, South, West }

// VECTOR3

public class Vector3Variable : ScriptableObject
{
    public Vector3 value;
}

[Serializable]
public class Vector3Reference
{
    public bool useConstant = true;
    public Vector3 constantValue;
    public Vector3Variable variable;

    public Vector3 value
    {
        get { return useConstant ? constantValue : variable.value; }
    }
}