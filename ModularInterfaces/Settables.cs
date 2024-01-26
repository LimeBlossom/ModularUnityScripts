using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISettableInt
{
    void SetInt(int setTo);
}

public interface ISettableFloat
{
    void SetFloat(float setTo);
}

public interface ISettableGameObject
{
    void SetGameObject(GameObject setTo);
    void SetGameObject(GameObject[] setTo);
}

public interface ISettableColor
{
    void SetColor(Color setTo);
}