using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float dashSpeed = 2;
    [SerializeField] private KeyCode[] leftCodes;
    [SerializeField] private KeyCode[] rightCodes;
    [SerializeField] private string[] leftKeys;
    [SerializeField] private string[] rightKeys;
    [SerializeField] private bool doubleTapKey;
    [SerializeField] private bool doubleTapButton;
    [SerializeField] private float doubleTapTimeDiffMax = .04f;
    [SerializeField] private float minTimeBetween = 0.5f;
    [SerializeField] private int maxAirDashes = 1;
    [SerializeField] private float floorCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool debug = false;
    [SerializeField] private MonoBehaviour[] actions;
    [SerializeField] private MonoBehaviour[] dashLeftActions;
    [SerializeField] private MonoBehaviour[] dashRightActions;
    [SerializeField] private List<string> objectsToIgnore;

    private int airDashesLeft = 1;
    private float lastDashTime = 0;

    private bool dashLeft = false;
    private bool dashRight = false;

    private string lastKey = "";
    private float lastKeyPressTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (rb == null)
        {
            Debug.LogError(gameObject.name + " sets velocity but does not have a rigidbody!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // KEYS
        foreach(string key in leftKeys)
        {
            if(Input.GetKeyDown(key))
            {
                if(doubleTapKey)
                {
                    if(lastKey == key)
                    {
                        if(lastKeyPressTime + doubleTapTimeDiffMax > Time.fixedTime)
                        {
                            dashLeft = true;
                        }
                    }
                    lastKeyPressTime = Time.fixedTime;
                }
                else
                {
                    dashLeft = true;
                }
                lastKey = key;
            }
        }
        foreach (string key in rightKeys)
        {
            if (Input.GetKeyDown(key))
            {
                if (doubleTapKey)
                {
                    if (lastKey == key)
                    {
                        if (lastKeyPressTime + doubleTapTimeDiffMax > Time.fixedTime)
                        {
                            dashRight = true;
                        }
                    }
                    lastKeyPressTime = Time.fixedTime;
                }
                else
                {
                    dashRight = true;
                }
                lastKey = key;
            }
        }

        // KEY CODES
        foreach (KeyCode key in leftCodes)
        {
            if(Input.GetKeyDown(key))
            {
                if(doubleTapButton)
                {
                    if(lastKey == key.ToString())
                    {
                        if(lastKeyPressTime + doubleTapTimeDiffMax > Time.fixedTime)
                        {
                            dashLeft = true;
                        }
                    }
                    lastKeyPressTime = Time.fixedTime;
                }
                else
                {
                    dashLeft = true;
                }
                lastKey = key.ToString();
            }
        }
        foreach (KeyCode key in rightCodes)
        {
            if (Input.GetKeyDown(key))
            {
                if (doubleTapButton)
                {
                    if (lastKey == key.ToString())
                    {
                        if (lastKeyPressTime + doubleTapTimeDiffMax > Time.fixedTime)
                        {
                            dashRight = true;
                        }
                    }
                    lastKeyPressTime = Time.fixedTime;
                }
                else
                {
                    dashRight = true;
                }
                lastKey = key.ToString();
            }
        }

        if (debug)
        {
            Collider2D collider = GetComponent<Collider2D>();

            Debug.DrawRay(new Vector2(collider.bounds.max.x, collider.bounds.max.y), Vector3.right * dashSpeed, Color.cyan);
            Debug.DrawRay(new Vector2(collider.bounds.max.x, collider.bounds.min.y), Vector3.right * dashSpeed, Color.cyan);

            Debug.DrawRay(new Vector2(collider.bounds.min.x, collider.bounds.max.y), -Vector3.right * dashSpeed, Color.cyan);
            Debug.DrawRay(new Vector2(collider.bounds.min.x, collider.bounds.min.y), -Vector3.right * dashSpeed, Color.cyan);
        }
    }

    private void FixedUpdate()
    {
        bool isGrounded = IsGrounded();
        if(isGrounded)
        {
            airDashesLeft = maxAirDashes;
        }

        if (dashLeft)
        {
            if(airDashesLeft > 0 && lastDashTime + minTimeBetween < Time.fixedTime)
            {
                lastDashTime = Time.fixedTime;
                if(!isGrounded)
                {
                    airDashesLeft--;
                }
                Dash(-dashSpeed);
            }
        }
        else if (dashRight)
        {
            if (airDashesLeft > 0 && lastDashTime + minTimeBetween < Time.fixedTime)
            {
                lastDashTime = Time.fixedTime;
                if (!isGrounded)
                {
                    airDashesLeft--;
                }
                Dash(dashSpeed);
            }
        }

        dashLeft = false;
        dashRight = false;
    }

    private void Dash(float xDirection)
    {
        Vector2 position = transform.position;
        Vector2 direction = xDirection > 0 ? Vector2.right: -Vector2.right;

        Vector2 finalPosition = position + direction * Mathf.Abs(xDirection);

        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        Collider2D collider = GetComponent<Collider2D>();

        if(xDirection > 0)
        {
            // Top right corner
            foreach(RaycastHit2D hit in Physics2D.RaycastAll(new Vector2(collider.bounds.max.x, collider.bounds.max.y), direction, Mathf.Abs(xDirection)))
            {
                hits.Add(hit);
            }

            // Bottom right corner
            foreach (RaycastHit2D hit in Physics2D.RaycastAll(new Vector2(collider.bounds.max.x, collider.bounds.min.y), direction, Mathf.Abs(xDirection)))
            {
                hits.Add(hit);
            }
            Activate(dashRightActions);
        }
        if (xDirection < 0)
        {
            // Top left corner
            foreach (RaycastHit2D hit in Physics2D.RaycastAll(new Vector2(collider.bounds.min.x, collider.bounds.max.y), direction, Mathf.Abs(xDirection)))
            {
                hits.Add(hit);
            }
            // Bottom left corner
            foreach (RaycastHit2D hit in Physics2D.RaycastAll(new Vector2(collider.bounds.min.x, collider.bounds.min.y), direction, Mathf.Abs(xDirection)))
            {
                hits.Add(hit);
            }
            Activate(dashLeftActions);
        }

        float shortestDistance = 1000;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                if (hit.distance < shortestDistance)
                {
                    bool skip = false;
                    foreach (string ignoreName in objectsToIgnore)
                    {
                        if (hit.collider.gameObject.name.Contains(ignoreName))
                        {
                            skip = true;
                        }
                    }
                    if(!skip)
                    {
                        if (debug)
                        {
                            Debug.Log(hit.collider.gameObject.name + " hit at distance: " + hit.distance);
                        }
                        shortestDistance = hit.distance;
                        finalPosition = position + direction * shortestDistance;
                    }
                }
            }
        }

        rb.velocity = new Vector2(0, 0);
        StartCoroutine(LerpPosition(finalPosition, 0.1f));
        Activate(actions);
    }

    IEnumerator LerpPosition(Vector2 targetPosition, float duration)
    {
        float time = 0;
        Vector2 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            rb.velocity = Vector2.zero;
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        rb.velocity = Vector2.zero;
    }

    private bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;

        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        // If we don't have box colliders to work with then calculate from the center of the object
        if (!GetComponent<BoxCollider2D>())
        {
            hits.Add(Physics2D.Raycast(position, direction, floorCheckDistance, groundLayer));

            if (debug)
            {
                Debug.DrawRay(position, Vector3.down * floorCheckDistance, Color.yellow);
            }
        }
        else
        {
            foreach (BoxCollider2D collider in GetComponents<BoxCollider2D>())
            {
                // Calculate from the bottom corners of each box collider
                hits.Add(Physics2D.Raycast(new Vector2(collider.bounds.max.x + collider.edgeRadius, collider.bounds.min.y), direction, floorCheckDistance, groundLayer));
                hits.Add(Physics2D.Raycast(new Vector2(collider.bounds.min.x - collider.edgeRadius, collider.bounds.min.y), direction, floorCheckDistance, groundLayer));

                // Calculate from the bottom center of the box collider
                hits.Add(Physics2D.Raycast(new Vector2(collider.bounds.center.x, collider.bounds.min.y), direction, floorCheckDistance, groundLayer));

                if (debug)
                {
                    Debug.DrawRay(new Vector2(collider.bounds.max.x + collider.edgeRadius, collider.bounds.min.y), Vector3.down * floorCheckDistance, Color.blue);
                    Debug.DrawRay(new Vector2(collider.bounds.min.x - collider.edgeRadius, collider.bounds.min.y), Vector3.down * floorCheckDistance, Color.blue);
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

    private void Activate(MonoBehaviour[] actions)
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
