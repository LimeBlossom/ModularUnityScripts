using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetPositionPercentage : MonoBehaviour
{
    [SerializeField] private Vector3 offsetScale;
    [SerializeField] private FloatReference offsetByAmount;
    [SerializeField] private FloatReference offsetMaximum;

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        float offset = offsetMaximum.value - offsetByAmount.value;

        transform.localPosition = originalPosition + new Vector3(offsetScale.x * offset, offsetScale.y * offset, offsetScale.z * offset) * 0.8f;
    }
}
