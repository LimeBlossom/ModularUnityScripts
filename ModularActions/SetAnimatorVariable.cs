using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimatorVariable : MonoBehaviour, IActivatable, ISettableFloat
{
    [SerializeField] private Animator animator;
    [SerializeField] private string variableName;
    [SerializeField] private bool isTrigger;
    [SerializeField] private bool isFloat;
    [SerializeField] private bool isInt;
    [SerializeField] private bool isBool;

    [SerializeField] private float floatValue;
    [SerializeField] private int intValue;
    [SerializeField] private bool boolValue;

    public void Activate()
    {
        if(isTrigger)
        {
            animator.SetTrigger(variableName);
        }
        if(isFloat)
        {
            animator.SetFloat(variableName, floatValue);
        }
        if(isInt)
        {
            animator.SetInteger(variableName, intValue);
        }
        if(isBool)
        {
            animator.SetBool(variableName, boolValue);
        }
    }

    public void SetFloat(float setTo)
    {
        floatValue = setTo;
    }
}
