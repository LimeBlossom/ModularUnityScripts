using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobbleAlpha : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private MeshRenderer meshRenderer;

    [SerializeField] private Color color;
    [SerializeField] private HSBColor hsbColor;

    [SerializeField] private MinMaxFloat wobbleFrequencyBounds;
    [SerializeField] private MinMaxFloat wobbleOffsetBounds;
    [SerializeField] private float minAlpha = 0;
    [SerializeField] private float maxAlpha = 1;

    private float wobbleFrequency;
    private float wobbleOffset;

    private float timeAlive = 0;

    void Start()
    {
        hsbColor = new HSBColor(color);
        wobbleFrequency = wobbleFrequencyBounds.Random(-1);
        wobbleOffset = wobbleOffsetBounds.Random(-1);
    }

    void Update()
    {
        timeAlive += Time.deltaTime;

        float curWobble = ((Mathf.Sin(timeAlive * wobbleFrequency + wobbleOffset) + 1f) / 2f) * (maxAlpha - minAlpha) + minAlpha;

        hsbColor.a = curWobble;

        if(spriteRenderer != null)
        {
            spriteRenderer.color = hsbColor.ToColor();
        }
        if(meshRenderer != null)
        {
            meshRenderer.material.color = hsbColor.ToColor();
        }
    }
}
