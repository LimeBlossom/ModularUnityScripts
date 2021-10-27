using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnHover : MonoBehaviour
{
    [SerializeField] private Color toColor;
    [SerializeField] private SpriteRenderer[] spriteRenderers;

    private Color[] originalColors;

    private void Start()
    {
        originalColors = new Color[spriteRenderers.Length];
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            originalColors[i] = spriteRenderers[i].color;
        }
    }

    private void OnMouseOver()
    {
        for(int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = toColor;
        }
    }

    private void OnMouseExit()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = originalColors[i];
        }
    }
}
