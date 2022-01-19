using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] private Transform rootObject, followObject;
    [SerializeField] private Vector3 positionOffset, rotationOffset, headBodyOffset;

    // Update is called once per frame
    void LateUpdate()
    {
        rootObject.position = transform.position + headBodyOffset;

        // Originally rootObject.forward = Vector3.ProjectOnPlane(followObject.up, Vector3.up).normalized; and no rotation after.  Hacked due to rotation of model's head
        rootObject.forward = Vector3.ProjectOnPlane(followObject.right, Vector3.up).normalized;
        rootObject.rotation = rootObject.rotation * Quaternion.Euler(new Vector3(0, -90, 0));

        transform.position = followObject.TransformPoint(positionOffset);
        transform.rotation = followObject.rotation * Quaternion.Euler(rotationOffset);
    }
}
