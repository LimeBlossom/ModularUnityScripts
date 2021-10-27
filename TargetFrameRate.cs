using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{
    [SerializeField] private int target;

    void Awake()
    {
        Application.targetFrameRate = target;
    }
}
