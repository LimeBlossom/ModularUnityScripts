using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField] private GameObject toRotate;
    [SerializeField] private Vector3 speed;

    private void Start()
    {
        if(toRotate == null)
        {
            toRotate = gameObject;
        }
    }
    void Update()
    {
        toRotate.transform.rotation = Quaternion.Euler(toRotate.transform.eulerAngles + speed * Time.deltaTime);
    }
}
