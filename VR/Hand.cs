using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    //[SerializeField] private float animationSpeed;

    // Animation
    //Animator animator;
    //private float gripTarget;
    //private float triggerTarget;
    //private float gripCurrent;
    //private float triggerCurrent;
    //private const string ANIMATORGRIPPARAM = "Grip";
    //private const string ANIMATORTRIGGERPARAM = "Trigger";

    // Physics Movement
    [SerializeField] private GameObject followObject;
    [SerializeField] private float followSpeed = 30f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;
    private Transform _followTarget;
    private Rigidbody _body;

    // Start is called before the first frame update
    void Start()
    {
        // Physics Movement
        _followTarget = followObject.transform;
        _body = GetComponent<Rigidbody>();
        _body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _body.mass = 20f;
        
        // Teleport hands
        _body.position = _followTarget.position;
        _body.rotation = _followTarget.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();

        PhysicsMove();
    }

    private void PhysicsMove()
    {
        // Position
        Vector3 positionWithOffset = _followTarget.TransformPoint(positionOffset);
        float distance = Vector3.Distance(positionWithOffset, transform.position);
        _body.velocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance);
        //_body.position = _followTarget.position;


        // Rotation
        Quaternion rotationWithOffset = _followTarget.rotation * Quaternion.Euler(rotationOffset);
        Quaternion q = rotationWithOffset * Quaternion.Inverse(_body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        _body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);
    }

    internal void SetGrip(float v)
    {
        //gripTarget = v;
    }

    internal void SetTrigger(float v)
    {
        //triggerTarget = v;
    }

    void AnimateHand()
    {
        //if(gripCurrent != gripTarget)
        //{
        //    gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * animationSpeed);
        //    animator.SetFloat(ANIMATORGRIPPARAM, gripCurrent);
        //}
        //if (triggerCurrent != triggerTarget)
        //{
        //    triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * animationSpeed);
        //    animator.SetFloat(ANIMATORTRIGGERPARAM, triggerCurrent);
        //}
    }
}
