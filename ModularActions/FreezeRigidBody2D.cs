using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRigidBody2D : MonoBehaviour, IActivatable
{
    [SerializeField] private bool onAwake;
    [SerializeField] private bool freezeXPos;
    [SerializeField] private bool freezeYPos;
    [SerializeField] private bool freezeZRot;

    private void Awake()
    {
        if(onAwake)
        {
            Activate();
        }
    }

    public void Activate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            if (freezeXPos)
            {
                rb.constraints |= RigidbodyConstraints2D.FreezePositionX;
            }
            if (freezeYPos)
            {
                rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
            }
            if (freezeZRot)
            {
                rb.constraints |= RigidbodyConstraints2D.FreezeRotation;
            }
        }
        else
        {
            Debug.LogError(name + " does not have a RigidBody2D");
        }
    }
}
