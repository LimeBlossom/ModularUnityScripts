using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
//using UnityEngine.Rendering.Universal;

public class ScaleOverTime : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject toScale;
    public Vector3 startingScale;
    public Vector3 endingScale;
    public float endTime;
    public bool selfDestructAfter;
    public bool transparentOverTime = false;

    private float curTime = 0;
    [SerializeField] private bool unscaledDeltaTime = false;

    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    public void Activate()
    {
        if(toScale == null)
        {
            toScale = gameObject;
        }
        StartCoroutine(DoScale());
    }

    IEnumerator DoScale()
    {
        while(curTime < endTime)
        {
            curTime += unscaledDeltaTime ? Time.unscaledDeltaTime : Time.deltaTime;
            Vector3 differenceInScale = endingScale - startingScale;
            float scalar = curTime / endTime;
            toScale.transform.localScale = startingScale + differenceInScale * scalar;

            if (transparentOverTime)
            {
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_AlphaMultiplier", 1 - curTime / endTime);
            }
            yield return new WaitForEndOfFrame();
        }

        curTime = 0;

        ActivateActions();

        if (selfDestructAfter)
        {
            if(gameObject.GetComponent<Renderer>())
            {
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_AlphaMultiplier", 1);
            }
            Destroy(gameObject);
        }
    }

    private void ActivateActions()
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
