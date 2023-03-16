using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LerpColor : MonoBehaviour, IActivatable
{
    [SerializeField] private float[] timeToLerp;
    [SerializeField] private Color[] changeColorTo;
    [SerializeField] private SpriteRenderer[] spriteToChange;
    [SerializeField] private Image[] imageToChange;

    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    [SerializeField] private bool unscaledTime = false;
    [SerializeField] private bool debug = false;
    [SerializeField] private float time = 0;

    public void Activate()
    {
        float tempTime = timeToLerp[Random.Range(0, timeToLerp.Length)];

        foreach (Image image in imageToChange)
        {
            StartCoroutine(LerpValues(
                image,
                tempTime,
                changeColorTo[Random.Range(0, changeColorTo.Length)]));
        }
        StartCoroutine(CoroutineUtils.WaitAndDo(tempTime, ()=> { ActivateNext(); }, unscaledTime = true));
    }

    IEnumerator LerpValues(Image image, float lerpTime, Color newColor)
    {
        time = 0;
        Color startValue = image.color;

        while (time < lerpTime)
        {
            if(unscaledTime)
            {
                time += Time.unscaledDeltaTime;
            }
            else
            {
                time += Time.deltaTime;
            }

            var tempR = Mathf.Lerp(startValue.r, newColor.r, time / lerpTime);
            var tempG = Mathf.Lerp(startValue.g, newColor.g, time / lerpTime);
            var tempB = Mathf.Lerp(startValue.b, newColor.b, time / lerpTime);
            var tempA = Mathf.Lerp(startValue.a, newColor.a, time / lerpTime);
            image.color = new Color(tempR, tempG, tempB, tempA);

            if (debug)
            {
                Debug.Log($"{image.color} {time}");
            }

            yield return null;
        }

        image.color = newColor;
    }

    public void ActivateNext()
    {
        events.Invoke();
        if (actions != null && actions.Length > 0)
        {
            foreach (IActivatable action in actions)
            {
                action.Activate();
            }
        }
    }
}
