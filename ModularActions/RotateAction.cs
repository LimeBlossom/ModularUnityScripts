using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAction : MonoBehaviour, IActivatable
{
    [SerializeField] private Transform[] toRotate;
    [SerializeField] private Vector3 rotation;

    [SerializeField] private float numberOfTimes = 1;

    [SerializeField] private bool timesDeltaTime = true;
    [SerializeField] private bool rotateToRotation = false;

    private bool activated = false;

    void Update()
    {
        if(activated)
        {
            if(numberOfTimes > 1)
            {
                Activate();
                numberOfTimes -= Time.timeScale;
            }
        }
    }

    public void Activate()
    {
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
                    t.Rotate(rotation * Time.timeScale);
                }
            }
        }
        else if(rotateToRotation)
        {
            foreach(Transform t in toRotate)
            {
                StartCoroutine(RotateTransform(t, rotation));
            }
        }
    }

    IEnumerator RotateTransform(Transform t, Vector3 r)
    {
        while(Vector3.Distance(t.eulerAngles, r) > 1)
        {
            t.eulerAngles = new Vector3(
             Mathf.LerpAngle(t.eulerAngles.x, r.x, Time.deltaTime),
             Mathf.LerpAngle(t.eulerAngles.y, r.y, Time.deltaTime),
             Mathf.LerpAngle(t.eulerAngles.z, r.z, Time.deltaTime));

            yield return new WaitForFixedUpdate();
        }
    }
}
