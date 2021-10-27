using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WobbleColor : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Text textObj;

    public HSBColor hsbColor;

    public float wobbleFrequency = 1;
    public float offset = 0;

    private float timeAlive = 0;

    void Update()
    {
        timeAlive += Time.deltaTime;

        float curWobble = (Mathf.Sin(timeAlive * wobbleFrequency + offset) + 1f) / 2f;

        hsbColor.h = curWobble;

        if(spriteRenderer != null)
        {
            spriteRenderer.color = hsbColor.ToColor();
        }
        if(textObj != null)
        {
            textObj.color = hsbColor.ToColor();
        }
    }
}
