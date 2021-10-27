using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSprite : MonoBehaviour, IActivatable
{
    public Sprite sprite;
    public SpriteRenderer spriteRenderer;

    public void Activate()
    {
        spriteRenderer.sprite = sprite;
    }
}
