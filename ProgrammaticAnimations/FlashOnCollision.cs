using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashOnCollision : MonoBehaviour
{
    public Color flashColor;
    public string[] objectsThatHurt;
    public SpriteRenderer[] sprites;

    public float flashSpeed;
    public float flashLength;

    private Color[] originalColors;

    private float activatedTime;

    void Start()
    {
        if (sprites.Length == 0)
        {
            sprites = new SpriteRenderer[1];
            sprites[0] = GetComponent<SpriteRenderer>();
        }

        originalColors = new Color[sprites.Length];
        for(int i = 0; i < sprites.Length; i++)
        {
            originalColors[i] = sprites[i].color;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < objectsThatHurt.Length; i++)
        {
            if (collision.gameObject.name.Contains(objectsThatHurt[i]))
            {
                activatedTime = Time.fixedTime;
                StartCoroutine(Flash());
            }
        }
    }

    IEnumerator Flash()
    {
        if(activatedTime + flashLength > Time.fixedTime)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                if (sprites[i].color == flashColor)
                {
                    sprites[i].color = originalColors[i];
                }
                else
                {
                    sprites[i].color = flashColor;
                }
            }
            yield return new WaitForSeconds(flashSpeed);
            StartCoroutine(Flash());
        }
        else
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].color = originalColors[i];
            }
        }
    }
}
