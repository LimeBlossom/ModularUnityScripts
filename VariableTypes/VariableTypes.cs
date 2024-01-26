using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public struct MinMaxFloat {
    public float min;
    public float max;
    public float Random(int seed)
    {
        System.Random random;
        if (seed != -1)
        {
            random = new System.Random(seed);
        }
        else
        {
            random = new System.Random();
        }

        return (float)random.NextDouble() * (max - min) + min;
    }
}
[System.Serializable]
public struct MinMaxInt { public int min; public int max; }
[System.Serializable]
public struct MinMaxVector3 {
    public Vector3 min;
    public Vector3 max;
    public Vector3 Random(int seed)
    {
        System.Random random;
        if(seed != -1)
        {
            random = new System.Random(seed);
        }
        else
        {
            random = new System.Random();
        }

        Vector3 toReturn;
        float nextDouble = (float)random.NextDouble();
        toReturn.x = nextDouble * (max.x - min.x) + min.x;
        toReturn.y = (float)random.NextDouble() * (max.y - min.y) + min.y;
        toReturn.z = (float)random.NextDouble() * (max.z - min.z) + min.z;
        return toReturn;
    }
}
public enum CardinalDirection { North, East, South, West }
public enum Direction { Up, Down, Left, Right, Forward, Backward }

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

public static class VariableTypes
{
    public static Vector3 GetDirection(Transform origin, Direction direction)
    {
        Vector3 dir = Vector3.zero;
        switch (direction)
        {
            case Direction.Up:
                dir = origin.up;
                break;
            case Direction.Down:
                dir = -origin.up;
                break;
            case Direction.Right:
                dir = origin.right;
                break;
            case Direction.Left:
                dir = -origin.right;
                break;
            case Direction.Forward:
                dir = origin.forward;
                break;
            case Direction.Backward:
                dir = -origin.forward;
                break;
            default:
                break;
        }
        return dir;
    }
}