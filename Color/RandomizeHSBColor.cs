using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeHSBColor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] renderers;
    [SerializeField] private HSBColor min;
    [SerializeField] private HSBColor max;

    private void Awake()
    {
        HSBColor temp = new HSBColor(Random.Range(min.h, max.h), Random.Range(min.s, max.s), Random.Range(min.b, max.b), Random.Range(min.a, max.a));
        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.color = temp.ToColor();
        }
    }
}
