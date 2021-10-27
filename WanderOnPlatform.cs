using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderOnPlatform : MonoBehaviour
{
    [SerializeField] private float walkSpeed = .4f;
    [SerializeField] private float floorCheckDistance = .1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool debug = false;
    [SerializeField] private MonoBehaviour[] moveLeftActions;
    [SerializeField] private MonoBehaviour[] moveRightActions;

    private Vector2 currentDirection;

    private void Awake()
    {
        currentDirection = Vector2.left;
    }

    private void Update()
    {
        bool isLeftGrounded = IsLeftGrounded();
        bool isRightGrounded = IsRightGrounded();
        bool hitRightWall = WallCheckRight();
        bool hitLeftWall = WallCheckLeft();

        if(!isLeftGrounded || hitLeftWall)
        {
            currentDirection = Vector2.right;
        }
        if(!isRightGrounded || hitRightWall)
        {
            currentDirection = -Vector2.right;
        }
        transform.Translate(currentDirection * walkSpeed * Time.deltaTime);
        if(currentDirection == Vector2.right)
        {
            Activate(moveRightActions);
        }
        else if(currentDirection == -Vector2.right)
        {
            Activate(moveLeftActions);
        }
    }

    bool WallCheckRight()
    {
        Vector2 direction = Vector2.right;

        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        foreach (Collider2D collider in GetComponents<Collider2D>())
        {
            // Calculate from the mid right side
            hits.Add(Physics2D.Raycast(new Vector2(collider.bounds.max.x, collider.bounds.center.y), direction, floorCheckDistance, groundLayer));

            if (debug)
            {
                Debug.DrawRay(new Vector2(collider.bounds.max.x, collider.bounds.center.y), direction * floorCheckDistance, Color.black);
            }
        }

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                return true;
            }
        }

        return false;
    }

    bool WallCheckLeft()
    {
        Vector2 direction = -Vector2.right;

        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        foreach (Collider2D collider in GetComponents<Collider2D>())
        {
            // Calculate from the mid right side
            hits.Add(Physics2D.Raycast(new Vector2(collider.bounds.min.x, collider.bounds.center.y), direction, floorCheckDistance, groundLayer));

            if (debug)
            {
                Debug.DrawRay(new Vector2(collider.bounds.min.x, collider.bounds.center.y), direction * floorCheckDistance, Color.black);
            }
        }

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                return true;
            }
        }

        return false;
    }

    bool IsLeftGrounded()
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
                // Calculate from the bottom left corner
                hits.Add(Physics2D.Raycast(collider.bounds.min, direction, floorCheckDistance, groundLayer));

                if (debug)
                {
                    Debug.DrawRay(collider.bounds.min, Vector3.down * floorCheckDistance, Color.black);
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

    bool IsRightGrounded()
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
                // Calculate from the bottom right corner
                hits.Add(Physics2D.Raycast(new Vector2(collider.bounds.max.x, collider.bounds.min.y), direction, floorCheckDistance, groundLayer));

                if (debug)
                {
                    Debug.DrawRay(new Vector2(collider.bounds.max.x, collider.bounds.min.y), Vector3.down * floorCheckDistance, Color.red);
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

    public void Activate(MonoBehaviour[] actions)
    {
        if (actions != null && actions.Length > 0)
        {
            foreach (IActivatable action in actions)
            {
                action.Activate();
            }
        }
    }
}
