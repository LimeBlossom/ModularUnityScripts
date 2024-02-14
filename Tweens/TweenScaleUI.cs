//using DG.Tweening;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.UI;


//public class TweenScaleUI : MonoBehaviour, IActivatable
//{
//    [SerializeField] private RectTransform toTween;
//    [SerializeField] private float endScale = 1;
//    [SerializeField] private float duration = 1;
//    [SerializeField] private UnityEvent onTweenEnd;

//    public void Activate()
//    {
//        if(toTween == null)
//        {
//            toTween = GetComponent<RectTransform>();
//        }
//        toTween.DOScale(endScale, duration);
//        StartCoroutine(OnTweenEnd());
//    }

//    IEnumerator OnTweenEnd()
//    {
//        yield return new WaitForSecondsRealtime(duration);
//        onTweenEnd.Invoke();
//    }
//}
