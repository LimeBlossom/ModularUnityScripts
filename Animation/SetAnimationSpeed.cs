using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimationSpeed : MonoBehaviour, IActivatable
{
    [SerializeField] private Animator animator;
    [SerializeField] private float speed;
    [SerializeField] private bool onStart;

    private void Start()
    {
        if(onStart)
            Activate();
    }
    public void Activate()
    {
        animator.speed = speed;
    }
}
