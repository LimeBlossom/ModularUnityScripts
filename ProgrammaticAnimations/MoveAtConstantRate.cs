using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAtConstantRate : MonoBehaviour
{
    [SerializeField] private Transform toMove;

    public Vector3 minSpeed;
    public Vector3 maxSpeed;

    public Vector3 direction;
    public float minRate;
    public float maxRate;

    public Vector3 speed;
    [SerializeField] private bool timesDeltaTime = true;


    private void Start()
    {
        if(toMove == null)
        {
            toMove = transform;
        }
        if(speed == Vector3.zero)
        {
            if(direction == Vector3.zero)
            {
                speed = new Vector3(Random.Range(minSpeed.x, maxSpeed.x), Random.Range(minSpeed.y, maxSpeed.y), Random.Range(minSpeed.z, maxSpeed.z));
            }
            else
            {
                speed = Random.Range(minRate, maxRate) * direction;
            }
        }
    }

    void Update()
    {
        Vector3 tempSpeed = speed;
        if (timesDeltaTime)
        {
            tempSpeed *= Time.deltaTime;
        }

        toMove.position = toMove.position + tempSpeed;
    }
}
