using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    public Vector3 speed;

    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles + speed * Time.deltaTime);
    }
}
