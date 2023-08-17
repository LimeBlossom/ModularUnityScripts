using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMovementController : MonoBehaviour {

    [SerializeField] private GameObject toFollow;
    [SerializeField] private Vector3 distance;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private bool smoothFollow;

    private bool foundPlayer = false;

    void Start()
    {
        transform.eulerAngles = rotation;
        FindPlayer();
    }

    void LateUpdate()
    {
        if (toFollow == null)
        {
            FindPlayer();
        }
        else
        {
            if(smoothFollow)
            {
                SmoothFollow();
            }
            else
            {
                transform.position = toFollow.transform.position + distance;
            }
        }
    }

    private void FindPlayer()
    {
        if (toFollow == null)
        {
            MovementController playerScript = FindObjectOfType<MovementController>();
            if (playerScript != null)
            {
                toFollow = playerScript.gameObject;
                if(!foundPlayer)
                {
                    transform.position = toFollow.transform.position + distance;
                }
                foundPlayer = true;
            }
        }
    }

    private void SmoothFollow()
    {
        transform.position = Vector3.Lerp(transform.position, toFollow.transform.position + distance, lerpSpeed);
    }
}
