using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct Bool3
{ public bool x; public bool y; public bool z; }

public class RotateAction : MonoBehaviour, IActivatable
{
    [SerializeField] private Transform[] toRotate;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Bool3 rotateAngles; 

    [SerializeField] private float numberOfTimes = 1;

    [SerializeField] private bool timesDeltaTime = true;
    [SerializeField] private bool rotateToRotation = false;

    private bool activated = false; 

    void Update()
    {
        if(activated)
        {
            if(numberOfTimes > 0)
            {
                Activate();
                numberOfTimes -= 1 * (timesDeltaTime ? Time.deltaTime : 1);
            }
        }
    }

    public void Activate()
    {
        if (numberOfTimes == 1 && !timesDeltaTime)
            numberOfTimes = 0;

        activated = true;
        if(!rotateToRotation)
        {
            foreach (Transform t in toRotate)
            {
                if (timesDeltaTime)
                {
                    t.Rotate(rotation * Time.deltaTime);
                }
                else
                {
                    t.Rotate(rotation);
                }
            }
        }
        else if(rotateToRotation)
        {
            foreach(Transform t in toRotate)
            {
                StartCoroutine(RotateTransform(t, rotation, rotateAngles));
            }
        }
    }

    IEnumerator RotateTransform(Transform t, Vector3 r, Bool3 b)
    {
        while(Vector3.Distance(t.eulerAngles, r) > 1)
        {
            t.eulerAngles = new Vector3(
             Mathf.LerpAngle(t.eulerAngles.x, b.x ? r.x : t.eulerAngles.x, Time.deltaTime),
             Mathf.LerpAngle(t.eulerAngles.y, b.y ? r.y : t.eulerAngles.y, Time.deltaTime),
             Mathf.LerpAngle(t.eulerAngles.z, b.z ? r.z : t.eulerAngles.z, Time.deltaTime)) ;

            yield return new WaitForFixedUpdate();
        }
    }
}
