using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomizeColor : MonoBehaviour, IActivatable
{
    [System.Serializable]
    public struct ColorRange { public Color min; public Color max; }
    [SerializeField] private ColorRange m_colorRange;

    [SerializeField] private Color[] colors;

    [SerializeField] private SpriteRenderer[] spriteToChange;
    [SerializeField] private Image[] imageToChange;

    public void Activate()
    {
        Color newColor = new Color(Random.Range(m_colorRange.min.r, m_colorRange.max.r), Random.Range(m_colorRange.min.g, m_colorRange.max.g), Random.Range(m_colorRange.min.b, m_colorRange.max.b));

        if(colors.Length > 0)
        {
            newColor = colors[Random.Range(0, colors.Length)];
        }

        foreach(SpriteRenderer spriteRenderer in spriteToChange)
        {
            spriteRenderer.color = newColor;
        }

        foreach (Image image in imageToChange)
        {
            image.color = newColor;
        }
    }
}
