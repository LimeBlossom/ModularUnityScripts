using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehindObject : MonoBehaviour
{
    [SerializeField] private GameObject follower;
    [SerializeField] private string toFollow;
    [SerializeField] private float offset;

    private GameObject target;

    private void Awake()
    {
        target = GameObject.Find(toFollow);
        if (target != null)
        {
            follower.transform.position = target.transform.position - target.transform.forward * offset;
        }
    }

    private void Update()
    {
        if (target != null)
        {
            follower.transform.position = target.transform.position - target.transform.forward * offset;
        }
        else
        {
            target = GameObject.Find(toFollow);
        }
    }
}
