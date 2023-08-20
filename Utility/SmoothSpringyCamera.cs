using UnityEngine;

public class SmoothSpringyCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 targetOffset = new Vector3(0, 2, -10);
    [SerializeField] private float distanceTolerance = 0.05f;
    [SerializeField] private float springConstant = 16f;
    [SerializeField] private float dampingConstant = 8f;
    [SerializeField] private float maxSpeed = 50f;
    [SerializeField] private Vector3 desiredRotation = new Vector3(45, 30, 0);
    [SerializeField] private float rotationSpeed = 5f;

    private Vector3 desiredPosition;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        FindPlayer();
    }

    private void LateUpdate()
    {
        if(target == null)
        {
            return;
        }

        desiredPosition = target.position + targetOffset;

        float distanceToTarget = Vector3.Distance(transform.position, desiredPosition);
        float movementSpeed = Mathf.Min(maxSpeed, springConstant * distanceToTarget) * Time.unscaledDeltaTime * Time.timeScale;

        if (distanceToTarget > distanceTolerance)
        {
            Vector3 springForce = (desiredPosition - transform.position) * springConstant;
            Vector3 dampingForce = -velocity * dampingConstant;
            Vector3 totalForce = springForce + dampingForce;
            velocity += totalForce * movementSpeed;
        }
        else
        {
            velocity = Vector3.zero;
        }

        transform.position += velocity;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(desiredRotation), rotationSpeed * Time.unscaledDeltaTime);
    }

    private void FindPlayer()
    {
        if (target == null)
        {
            //MovementController playerScript = FindObjectOfType<MovementController>();
            //if (playerScript != null)
            {
                //target = playerScript.transform;
            }
            if(target != null)
            {
                desiredPosition = target.position + targetOffset;
                transform.position = desiredPosition;
                transform.rotation = Quaternion.Euler(desiredRotation);
            }
        }
    }
}
