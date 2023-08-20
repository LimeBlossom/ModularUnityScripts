using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMove : MonoBehaviour {

    public Vector3 direction;
    public Vector3 rotation;
    public bool scaleByTime = true;


    void Update()
    {
        Vector3 toTranslate = direction;
        Vector3 toRotate = rotation;
        if (scaleByTime)
        {
            toTranslate *= Time.timeScale;
            toRotate *= Time.timeScale;
        }
        transform.Translate(toTranslate);
        transform.Rotate(toRotate);
    }
}
