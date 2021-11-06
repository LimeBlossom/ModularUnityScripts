using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpColor : MonoBehaviour, IActivatable
{
    [SerializeField] private float timeToLerp;
    [SerializeField] private Color changeColorTo;
    [SerializeField] private SpriteRenderer spriteToChange;

    public void Activate()
    {
        StartCoroutine(LerpValues());
    }

    IEnumerator LerpValues()
    {
        float time = 0;
        Color startValue = spriteToChange.color;

        while (time < timeToLerp)
        {
            var tempR = Mathf.Lerp(startValue.r, changeColorTo.r, time / timeToLerp);
            var tempG = Mathf.Lerp(startValue.g, changeColorTo.g, time / timeToLerp);
            var tempB = Mathf.Lerp(startValue.b, changeColorTo.b, time / timeToLerp);
            spriteToChange.color = new Color(tempR, tempG, tempB);
            time += Time.deltaTime;
            yield return null;
        }

        spriteToChange.color = changeColorTo;
    }
}
