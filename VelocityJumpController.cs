using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityJumpController : MonoBehaviour
{
    [SerializeField] private string[] leftKey;
    [SerializeField] private string[] rightKey;
    [SerializeField] private string[] jumpKey;
    [SerializeField] private float leftSpeed = .1f;
    [SerializeField] private float rightSpeed = .1f;
    [SerializeField] private float jumpSpeed = .1f;
    [SerializeField] private float floorCheckDistance = 0.1f;

    [SerializeField] private float airGravity = 1;
    [SerializeField] private float landGravity = 3;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private bool debug;

    private Rigidbody2D rigidbody;

    private bool isGrounded = false;
    private bool moveRight;
    private bool moveLeft;
    private bool jump;
    private bool jumpReleased;

    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<Rigidbody2D>())
        {
            Debug.LogError(gameObject.name + " sets velocity but does not have a rigidbody!");
        }
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (string key in jumpKey)
        {
            if (Input.GetKey(key))
            {
                jump = true;
            }
            if (Input.GetKeyUp(key))
            {
                jumpReleased = true;
                jump = false;
            }
        }
        foreach (string key in leftKey)
        {
            if (Input.GetKey(key))
            {
                moveLeft = true;
            }
        }
        foreach (string key in rightKey)
        {
            if (Input.GetKey(key))
            {
                moveRight = true;
            }
        }
    }

    // Fixed Update is called every 0.02 seconds
    private void FixedUpdate()
    {
        isGrounded = IsGrounded();

        // Jump
        if (jump)
        {
            if (isGrounded && jumpReleased)
            {
                jumpReleased = false;

                rigidbody.gravityScale = airGravity;

                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpSpeed);

                if (moveLeft)
                {
                    rigidbody.velocity = new Vector2(-leftSpeed * 50f * Time.fixedDeltaTime, rigidbody.velocity.y);
                }
                if (moveRight)
                {
                    rigidbody.velocity = new Vector2(rightSpeed * 50f * Time.fixedDeltaTime, rigidbody.velocity.y);
                }
            }
        }
        else
        {
            rigidbody.gravityScale = landGravity;
        }
    }

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;

        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        // If we don't have box colliders to work with then calculate from the center of the object
        if (!GetComponent<Collider2D>())
        {
            hits.Add(Physics2D.Raycast(position, direction, floorCheckDistance, groundLayer));

            if (debug)
            {
                Debug.DrawRay(position, Vector3.down * floorCheckDistance, Color.yellow);
            }
        }
        else
        {
            foreach (Collider2D collider in GetComponents<Collider2D>())
            {
                // Calculate from the bottom corners of each box collider
                hits.Add(Physics2D.Raycast(new Vector2(collider.bounds.max.x, collider.bounds.min.y), direction, floorCheckDistance, groundLayer));
                hits.Add(Physics2D.Raycast(collider.bounds.min, direction, floorCheckDistance, groundLayer));

                // Calculate from the bottom center of the box collider
                hits.Add(Physics2D.Raycast(new Vector2(collider.bounds.center.x, collider.bounds.min.y), direction, floorCheckDistance, groundLayer));

                if (debug)
                {
                    Debug.DrawRay(new Vector2(collider.bounds.max.x, collider.bounds.min.y), Vector3.down * floorCheckDistance, Color.blue);
                    Debug.DrawRay(collider.bounds.min, Vector3.down * floorCheckDistance, Color.blue);
                    Debug.DrawRay(new Vector2(collider.bounds.center.x, collider.bounds.min.y), Vector3.down * floorCheckDistance, Color.blue);
                }
            }
        }

        // Don't forget to increase the floorCheckDistance

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                return true;
            }
        }

        return false;
    }
}
