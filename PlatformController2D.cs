using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController2D : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public bool debug = false;
    public bool onDown = false;

    public float leftSpeed = 1f;
    public float rightSpeed = 1f;
    public float upSpeed = 1f;
    public float downSpeed = 1f;
    public float jumpSpeed = 5f;
    public float sprintMultiplier = 2f;
    public float floorCheckDistance = 0.1f;

    public float airGravity = 1;
    public float landGravity = 3;

    [SerializeField] private int maxAirJumps = 0;
    private int curAirJumps = 0;

    public string[] leftKey;
    public string[] rightKey;
    public string[] upKey;
    public string[] downKey;
    public string[] jumpKey;
    public string[] sprintKey;

    public KeyCode[] leftCode;
    public KeyCode[] rightCode;
    public KeyCode[] jumpCode;
    public KeyCode[] sprintCode;

    public string[] leftRightAxis;

    [SerializeField] private Sprite[] walkLeftSprites;
    [SerializeField] private Sprite[] walkRightSprites;
    [SerializeField] private Sprite[] jumpSprites;
    [SerializeField] private float spriteChangeRate;

    [SerializeField] private GameObject[] spawnOnJump;
    [SerializeField] private GameObject spawnPos;

    public LayerMask groundLayer;

    private bool isGrounded = false;
    private bool lastFrameInAir = false;
    private Rigidbody2D rb;
    private float lastSpriteFlip;
    private int spriteIndex = 0;

    private bool jump;
    private bool sprint;
    private bool moveRight;
    private bool moveLeft;
    //private bool moveUp;
    //private bool moveDown;

    private bool jumpReleased = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!GetComponent<Rigidbody2D>())
        {
            Debug.LogError(gameObject.name + " sets velocity but does not have a rigidbody!");
        }
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        lastSpriteFlip = Time.fixedTime;
    }

    // Update is called once per frame
    void Update()
    {
        // KEYS
        foreach(string key in sprintKey)
        {
            if(Input.GetKey(key))
            {
                sprint = true;
            }
        }
        foreach(string key in jumpKey)
        {
            if(Input.GetKey(key))
            {
                jump = true;
            }
            if(Input.GetKeyUp(key))
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
        foreach (string key in upKey)
        {
            if (Input.GetKey(key))
            {
                //moveUp = true;
            }
        }
        foreach (string key in downKey)
        {
            if (Input.GetKey(key))
            {
                //moveDown = true;
            }
        }

        //CODES
        foreach (KeyCode key in sprintCode)
        {
            if (Input.GetKey(key))
            {
                sprint = true;
            }
        }
        foreach (KeyCode key in jumpCode)
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
        foreach (KeyCode key in leftCode)
        {
            if (Input.GetKey(key))
            {
                moveLeft = true;
            }
        }
        foreach (KeyCode key in rightCode)
        {
            if (Input.GetKey(key))
            {
                moveRight = true;
            }
        }

        //AXIS
        foreach (string axis in leftRightAxis)
        {
            if(Input.GetAxisRaw(axis) > 0)
            {
                moveRight = true;
            }
            else if(Input.GetAxisRaw(axis) < 0)
            {
                moveLeft = true;
            }
        }
    }

    // Fixed Update is called every 0.02 seconds
    private void FixedUpdate()
    {
        lastFrameInAir = !isGrounded;
        isGrounded = IsGrounded();

        if(isGrounded)
        {
            curAirJumps = maxAirJumps;
        }

        // Jump
        if (jump)
        { 
            if((isGrounded && jumpReleased) || (!isGrounded && jumpReleased && curAirJumps > 0))
            {
                if(!isGrounded)
                {
                    curAirJumps -= 1;
                }

                jumpReleased = false;

                rb.gravityScale = airGravity;

                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpSpeed);

                if (moveLeft)
                {
                    float tempSpeed = -leftSpeed * 50f;
                    if(sprint)
                    {
                        tempSpeed *= sprintMultiplier;
                    }
                    rb.velocity = new Vector2(tempSpeed * Time.fixedDeltaTime, rb.velocity.y);
                }
                if (moveRight)
                {
                    float tempSpeed = rightSpeed * 50f;
                    if (sprint)
                    {
                        tempSpeed *= sprintMultiplier;
                    }
                    rb.velocity = new Vector2(tempSpeed * Time.fixedDeltaTime, rb.velocity.y);
                }

                foreach(GameObject toSpawn in spawnOnJump)
                {
                    GameObject spawned = Instantiate(toSpawn);
                    if(spawnPos != null)
                    {
                        spawned.transform.position = spawnPos.transform.position;
                    }
                }

                if(spriteRenderer && jumpSprites.Length > 0)
                {
                    lastSpriteFlip = Time.fixedTime;
                    spriteIndex = 0;
                    spriteRenderer.sprite = jumpSprites[spriteIndex];
                }
            }
            if (Time.fixedTime > spriteChangeRate + lastSpriteFlip && jumpSprites.Length > 0)
            {
                spriteIndex += 1;
                if (spriteIndex >= jumpSprites.Length)
                {
                    spriteIndex = 0;
                }
                spriteRenderer.sprite = jumpSprites[spriteIndex];
                lastSpriteFlip = Time.fixedTime;
            }
        }
        else
        {
            rb.gravityScale = landGravity;
            if(isGrounded && lastFrameInAir && walkRightSprites.Length > 0)
            {
                spriteIndex = 0;
                spriteRenderer.sprite = walkLeftSprites[spriteIndex];
                lastSpriteFlip = Time.fixedTime;
            }
        }
        
        if(moveLeft)
        {
            if (isGrounded)
            {
                float tempSpeed = -leftSpeed;
                if(sprint)
                {
                    tempSpeed *= sprintMultiplier;
                }
                transform.position += new Vector3(tempSpeed * Time.fixedDeltaTime, 0);
                if (walkLeftSprites.Length > 0 && Time.fixedTime > spriteChangeRate + lastSpriteFlip)
                {
                    spriteIndex += 1;
                    if (spriteIndex >= walkLeftSprites.Length)
                    {
                        spriteIndex = 0;
                    }
                    spriteRenderer.sprite = walkLeftSprites[spriteIndex];
                    lastSpriteFlip = Time.fixedTime;
                }
            }
            else
            {
                if (rb != null)
                {
                    if(rb.velocity.x > -leftSpeed)
                    {
                        rb.AddForce(new Vector2(-leftSpeed * 2 * Time.fixedDeltaTime, 0), ForceMode2D.Impulse);
                    }
                }
            }
        }

        if (moveRight)
        {
            if (isGrounded)
            {
                float tempSpeed = rightSpeed;
                if (sprint)
                {
                    tempSpeed *= sprintMultiplier;
                }
                transform.position += new Vector3(tempSpeed * Time.fixedDeltaTime, 0);
                if (walkRightSprites.Length > 0 && Time.fixedTime > spriteChangeRate + lastSpriteFlip)
                {
                    spriteIndex += 1;
                    if (spriteIndex >= walkRightSprites.Length)
                    {
                        spriteIndex = 0;
                    }
                    spriteRenderer.sprite = walkRightSprites[spriteIndex];
                    lastSpriteFlip = Time.fixedTime;
                }
            }
            else
            {
                if (rb != null)
                {
                    if(rb.velocity.x < rightSpeed)
                    {
                        rb.AddForce(new Vector2(rightSpeed * 2 * Time.fixedDeltaTime, 0), ForceMode2D.Impulse);
                    }
                }
            }
        }

        jump = false;
        sprint = false;
        moveLeft = false;
        moveRight = false;
        //moveUp = false;
        //moveDown = false;
    }

    bool IsGrounded()
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
}
