using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipBetweenSprites : MonoBehaviour
{
    public Sprite[] sprites;
    public float flipRate;

    private float lastFlip;
    private int spriteIndex = 0;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastFlip = Time.fixedTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime > flipRate + lastFlip)
        {
            spriteRenderer.sprite = sprites[spriteIndex];
            lastFlip = Time.fixedTime;
            spriteIndex += 1;
            if (spriteIndex >= sprites.Length)
            {
                spriteIndex = 0;
            }
        }
    }
}
