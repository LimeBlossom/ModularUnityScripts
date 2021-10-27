using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlatformController2D : MonoBehaviour
{
    public bool onDown = false;

    public float leftSpeed = .1f;
    public float rightSpeed = .1f;
    public float upSpeed = .1f;
    public float downSpeed = .1f;
    public float jumpSpeed = .1f;
    public float floorCheckDistance = 0.5f;

    public string[] leftKey;
    public string[] rightKey;
    public string[] upKey;
    public string[] downKey;
    public string[] jumpKey;

    public LayerMask groundLayer;

    public string[] tagsToNotMoveInto;

    public bool debug = false;

    private bool isGrounded = false;

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;

        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        hits.Add(Physics2D.Raycast(position, direction, floorCheckDistance, groundLayer));
        if(GetComponent<BoxCollider2D>())
        {
            BoxCollider2D boxy = GetComponent<BoxCollider2D>();
            hits.Add(Physics2D.Raycast(position + new Vector2(boxy.size.x / 2 + boxy.offset.x, 0), direction, floorCheckDistance, groundLayer));
            hits.Add(Physics2D.Raycast(position + new Vector2(-boxy.size.x / 2 + boxy.offset.x, 0), direction, floorCheckDistance, groundLayer));
        }

        // Don't forget to increase the floorCheckDistance

        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                return true;
            }
        }

        return false;
    }

    void Update()
    {
        isGrounded = IsGrounded();

        if (debug)
        {
            float tempHeight = GetComponent<BoxCollider2D>().bounds.extents.y;
            float tempWidth = GetComponent<BoxCollider2D>().bounds.extents.x;
            Debug.DrawRay(transform.position + new Vector3(0, tempHeight * 2), Vector3.up * upSpeed, Color.red);
            Debug.DrawRay(transform.position + new Vector3(0, 0), Vector3.down * downSpeed, Color.red);
            Debug.DrawRay(transform.position + new Vector3(-tempWidth, 0), Vector3.left * leftSpeed, Color.red);
            Debug.DrawRay(transform.position + new Vector3(tempWidth, 0), Vector3.right * rightSpeed, Color.red);
        }

        // Jump
        for (int i = 0; i < jumpKey.Length; i++)
        {
            if (Input.GetKeyDown(jumpKey[i]) && isGrounded)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpSpeed);
                //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse); //Jumping gets weighed down by downward momentum
                //transform.Translate(new Vector3(0, jumpSpeed)); //Teleport up
            }
        }

        // LEFT
        for (int i = 0; i < leftKey.Length; i++)
        {
            if (onDown)
            {
                if (Input.GetKeyDown(leftKey[i]))
                {
                    if (tagsToNotMoveInto != null)
                    {
                        if (GetComponent<BoxCollider2D>() != null)
                        {
                            float width = GetComponent<BoxCollider2D>().bounds.extents.x;
                            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + new Vector3(-width, 0), Vector3.left, leftSpeed);
                            foreach (RaycastHit2D hit in hits)
                            {
                                foreach (string tag in tagsToNotMoveInto)
                                {
                                    if (hit.collider.tag == tag)
                                    {
                                        return;
                                    }
                                }
                            }
                            transform.Translate(new Vector3(-leftSpeed, 0));
                        }
                    }
                    else
                    {
                        transform.Translate(new Vector3(-leftSpeed, 0));
                    }
                }
            }
            else
            {
                if (Input.GetKey(leftKey[i]))
                {
                    transform.position += new Vector3(-leftSpeed * Time.deltaTime, 0);
                }
            }
        }

        // RIGHT
        for (int i = 0; i < rightKey.Length; i++)
        {
            if (onDown)
            {
                if (Input.GetKeyDown(rightKey[i]))
                {
                    if (tagsToNotMoveInto != null)
                    {
                        if (GetComponent<BoxCollider2D>() != null)
                        {
                            float width = GetComponent<BoxCollider2D>().bounds.extents.x;
                            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + new Vector3(width, 0), Vector3.right, rightSpeed);
                            foreach (RaycastHit2D hit in hits)
                            {
                                foreach (string tag in tagsToNotMoveInto)
                                {
                                    if (hit.collider.tag == tag)
                                    {
                                        return;
                                    }
                                }
                            }
                            transform.Translate(new Vector3(rightSpeed, 0));
                        }
                    }
                    else
                    {
                        transform.position = transform.position + new Vector3(rightSpeed, 0);
                    }
                }
            }
            else
            {
                if (Input.GetKey(rightKey[i]))
                {
                    transform.position += new Vector3(rightSpeed * Time.deltaTime, 0);
                }
            }
        }

        // UP
        for (int i = 0; i < upKey.Length; i++)
        {
            if (onDown)
            {
                if (Input.GetKeyDown(upKey[i]))
                {
                    if (tagsToNotMoveInto != null)
                    {
                        if (GetComponent<BoxCollider2D>() != null)
                        {
                            float height = GetComponent<BoxCollider2D>().bounds.extents.y;
                            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + new Vector3(0, height * 2), Vector3.up, upSpeed);
                            Debug.Log(hits.Length);
                            foreach (RaycastHit2D hit in hits)
                            {
                                foreach (string tag in tagsToNotMoveInto)
                                {
                                    if (hit.collider.tag == tag)
                                    {
                                        return;
                                    }
                                }
                            }
                            transform.Translate(new Vector3(0, upSpeed));
                        }
                    }
                    else
                    {
                        transform.position += new Vector3(0, upSpeed);
                    }
                }
            }
            else
            {
                if (Input.GetKey(upKey[i]))
                {
                    transform.position += new Vector3(0, upSpeed * Time.deltaTime);
                }
            }
        }

        // DOWN
        for (int i = 0; i < downKey.Length; i++)
        {
            if (onDown)
            {
                if (Input.GetKeyDown(downKey[i]))
                {
                    if (tagsToNotMoveInto != null)
                    {
                        if (GetComponent<BoxCollider2D>() != null)
                        {
                            float height = GetComponent<BoxCollider2D>().bounds.extents.y;
                            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + new Vector3(0, 0), -Vector3.up, downSpeed);
                            foreach (RaycastHit2D hit in hits)
                            {
                                foreach (string tag in tagsToNotMoveInto)
                                {
                                    if (hit.collider.tag == tag)
                                    {
                                        return;
                                    }
                                }
                            }
                            transform.position += new Vector3(0, -downSpeed);
                        }
                        else
                        {
                            transform.position += new Vector3(0, -downSpeed);
                        }
                    }
                }
            }
            else
            {
                if (Input.GetKey(downKey[i]))
                {
                    transform.position += new Vector3(0, -downSpeed * Time.deltaTime);
                }
            }
        }
    }
}
