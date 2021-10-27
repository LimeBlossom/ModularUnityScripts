using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardConstantly : MonoBehaviour
{
    [SerializeField] private bool threeDimensional;
    public MinMaxFloat speed;
    private float curSpeed;

    public void SetSpeed(float newSpeed)
    {
        curSpeed = newSpeed;
    }

    private void Start()
    {
        curSpeed = Random.Range(speed.min, speed.max);
    }

    void Update()
    {
        if(!threeDimensional)
        {
            transform.position += transform.up * curSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += transform.forward * curSpeed * Time.deltaTime;
        }
    }
}
