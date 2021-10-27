using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSpritesOnKey : MonoBehaviour
{
    public Sprite[] sprites;
    public float flipRate;

    public string[] keyInput;

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
        bool flipSprite = false;
        if(Time.fixedTime > flipRate + lastFlip)
        {
            if(keyInput.Length > 0)
            {
                foreach(string input in keyInput)
                {
                    if (Input.GetKey(input))
                    {
                        flipSprite = true;
                    }
                }
            }
            else
            {
                flipSprite = true;
            }

            if(flipSprite)
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
}
