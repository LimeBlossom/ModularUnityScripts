using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobbleAlpha : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Color color;
    public HSBColor hsbColor;

    public float wobbleFrequency = 1;
    public float wobbleOffset = 0;
    public float minAlpha = 0;
    public float maxAlpha = 1;

    private float timeAlive = 0;

    void Start()
    {
        hsbColor = new HSBColor(color);
    }

    void Update()
    {
        timeAlive += Time.deltaTime;

        float curWobble = ((Mathf.Sin(timeAlive * wobbleFrequency + wobbleOffset) + 1f) / 2f) * (maxAlpha - minAlpha) + minAlpha;

        hsbColor.a = curWobble;

        spriteRenderer.color = hsbColor.ToColor();
    }
}
