using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimationState : MonoBehaviour, IActivatable
{
    [SerializeField] private Animator animator;
    [SerializeField] public List<StringAnimationState> stringSetters = new List<StringAnimationState>();
    [SerializeField] public List<IntegerAnimationState> intSetters = new List<IntegerAnimationState>();
    [SerializeField] public List<BoolAnimationState> boolSetters = new List<BoolAnimationState>();
    [SerializeField] public List<TriggerAnimationState> triggerSetters = new List<TriggerAnimationState>();

    public void Activate()
    {
        foreach(AnimationStateBase animSetter in stringSetters)
        {
            animSetter.ApplyState(animator);
        }
        foreach (AnimationStateBase animSetter in intSetters)
        {
            animSetter.ApplyState(animator);
        }
        foreach (AnimationStateBase animSetter in boolSetters)
        {
            animSetter.ApplyState(animator);
        }
        foreach (AnimationStateBase animSetter in triggerSetters)
        {
            animSetter.ApplyState(animator);
        }
    }
}

[System.Serializable]
public abstract class AnimationStateBase
{
    public abstract void ApplyState(Animator animator);
}

[System.Serializable]
public class StringAnimationState : AnimationStateBase
{
    [SerializeField] private string stateName;

    public override void ApplyState(Animator animator)
    {
        animator.Play(stateName);
    }
}

[System.Serializable]
public class IntegerAnimationState : AnimationStateBase
{
    [SerializeField] private string stateName;
    [SerializeField] private int stateValue;

    public override void ApplyState(Animator animator)
    {
        animator.SetInteger(stateName, stateValue);
    }
}

[System.Serializable]
public class BoolAnimationState : AnimationStateBase
{
    [SerializeField] private string stateName;
    [SerializeField] private bool stateValue;

    public override void ApplyState(Animator animator)
    {
        animator.SetBool(stateName, stateValue);
    }
}

[System.Serializable]
public class TriggerAnimationState : AnimationStateBase
{
    [SerializeField] private string stateName;

    public override void ApplyState(Animator animator)
    {
        animator.SetTrigger(stateName);
    }
}
