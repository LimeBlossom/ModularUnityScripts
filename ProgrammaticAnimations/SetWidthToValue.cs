using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWidthToValue : MonoBehaviour
{
    [SerializeField] private FloatReference floatReference;
    [SerializeField] private RectTransform rectTransform;

    void Update()
    {
        rectTransform.sizeDelta =
            new Vector2(floatReference.value, rectTransform.sizeDelta.y);
    }
}
