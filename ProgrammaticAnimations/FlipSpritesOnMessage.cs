using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSpritesOnMessage : MonoBehaviour
{
    public Sprite[] sprites;
    public float flipRate;

    public string[] messages;

    private float lastFlip;
    private int spriteIndex = 0;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastFlip = Time.fixedTime;
    }

    void OnEnable()
    {
        MessageHandler.OnMessage += RecieveMessage;
    }

    private void OnDisable()
    {
        MessageHandler.OnMessage -= RecieveMessage;
    }

    public void RecieveMessage(string type, int num)
    {
        foreach(string message in messages)
        {
            if (type == message)
            {
                FlipSprites();
            }
        }
    }

    private void FlipSprites()
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
