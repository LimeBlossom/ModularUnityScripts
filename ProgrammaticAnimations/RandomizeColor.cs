using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeColor : MonoBehaviour
{
    [System.Serializable]
    public struct ColorRange { public Color min; public Color max; }
    public ColorRange m_colorRange;

    [SerializeField] private SpriteRenderer toColor;

    void Awake()
    {
        Color newColor = new Color(Random.Range(m_colorRange.min.r, m_colorRange.max.r), Random.Range(m_colorRange.min.g, m_colorRange.max.g), Random.Range(m_colorRange.min.b, m_colorRange.max.b));

        if(toColor == null)
        {
            transform.GetComponent<SpriteRenderer>().color = newColor;
        }
        else
        {
            toColor.color = newColor;
        }
    }
}
