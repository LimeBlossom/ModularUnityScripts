using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpFloatVariable : MonoBehaviour, IActivatable
{
    public FloatReference toLerp;
    public float amount;

    public void Activate()
    {
        StartCoroutine(LerpValue(toLerp.value + amount));
    }

    IEnumerator LerpValue(float targetValue, float duration = 1)
    {
        float time = 0;
        float startValue = toLerp.value;

        while (time < duration)
        {
            toLerp.variable.value = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
