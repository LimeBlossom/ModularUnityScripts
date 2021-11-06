using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSpriteAction : MonoBehaviour, IActivatable
{
    [SerializeField] private Color flashColor;
    [SerializeField] private SpriteRenderer[] sprites;

    [SerializeField] private float flashSpeed;
    [SerializeField] private float flashLength;

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
        for (int i = 0; i < sprites.Length; i++)
        {
            originalColors[i] = sprites[i].color;
        }
    }

    public void Activate()
    {
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        if (activatedTime + flashLength > Time.fixedTime)
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
