using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private string toFollow;

    private GameObject target;

    private void Awake()
    {
        target = GameObject.Find(toFollow);
        if (target != null)
        {
            transform.position = target.transform.position;
        }
    }

    private void Update()
    {
        if (target != null)
        {
            transform.position = target.transform.position;
        }
        else
        {
            target = GameObject.Find(toFollow);
        }
    }
}
