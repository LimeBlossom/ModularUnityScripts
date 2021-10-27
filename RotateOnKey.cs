using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnKey : MonoBehaviour
{
    [SerializeField] private bool onDown = false;
    public Vector3 rotation;
    public string[] inputKey;
    public string[] speedBoostKey;
    public float speedBoostMulti;

    // Update is called once per frame
    void Update()
    {
        bool activate = false;
        foreach (string input in inputKey)
        {
            if(onDown && Input.GetKeyDown(input))
            {
                activate = true;
            }
            else if (!onDown && Input.GetKey(input))
            {
                activate = true;
            }
        }
        if(activate)
        {
            float boost = 1;
            foreach (string booster in speedBoostKey)
            {
                if (Input.GetKey(booster))
                {
                    boost = speedBoostMulti;
                }
            }
            Vector3 totalRotation = rotation * boost;
            if(!onDown)
            {
                totalRotation *= Time.deltaTime;
            }
            transform.Rotate(totalRotation);
        }
    }
}
