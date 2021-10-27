using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDistanceJoint2D : MonoBehaviour, IActivatable
{
    [SerializeField] private DistanceJoint2D joint;
    [SerializeField] private string targetTag;

    public void Activate()
    {
        GameObject target = GameObject.FindGameObjectWithTag(targetTag);
        if(target != null)
        {
            Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                joint.connectedBody = rb;
                joint.autoConfigureDistance = false;
            }
        }
    }
}
