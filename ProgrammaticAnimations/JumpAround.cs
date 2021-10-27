using UnityEngine;
using System.Collections;

public class JumpAround : MonoBehaviour
{
    public MinMaxFloat jumpTimeRange;
    public MinMaxFloat jumpForceRangeX;
    public MinMaxFloat jumpForceRangeY;
    
    private float jumpTimer;
    private bool canJump = false;

    void Start()
    {
        jumpTimer = Random.Range(jumpTimeRange.min, jumpTimeRange.max);
    }

    void Update()
    {
        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }
        if (canJump && jumpTimer <= 0)
        {
            Jump();
            jumpTimer = Random.Range(jumpTimeRange.min, jumpTimeRange.max);
            canJump = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == gameObject)
        {
            return;
        }
        canJump = true;
    }

    void OnCollisoinExit2D(Collision2D col)
    {
        canJump = false;
    }

    public void setJumpTimer(float time)
    {
        jumpTimer = time;
    }

    public void Jump()
    {
        if (GetComponent<Rigidbody2D>().velocity.magnitude < 3)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(jumpForceRangeX.min, jumpForceRangeX.max), Random.Range(jumpForceRangeY.min, jumpForceRangeY.max)), ForceMode2D.Impulse);
            canJump = false;
        }
    }
}