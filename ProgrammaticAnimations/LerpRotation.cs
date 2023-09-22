using System.Collections;
using UnityEngine;

public class LerpRotation : MonoBehaviour, IActivatable
{
    public Transform targetTransform;
    public Vector3 rotationAmount;
    public float lerpDuration = 1.0f;

    private Quaternion initialRotation;
    private Quaternion targetRotation;

    public void Activate()
    {
        if (targetTransform == null)
        {
            Debug.LogError("Target Transform is not assigned!");
            enabled = false;
            return;
        }

        initialRotation = targetTransform.rotation;
        targetRotation = initialRotation * Quaternion.Euler(rotationAmount);

        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        float startTime = Time.time;
        while (Time.time - startTime < lerpDuration)
        {
            float lerpProgress = (Time.time - startTime) / lerpDuration;
            targetTransform.rotation = Quaternion.Lerp(initialRotation, targetRotation, lerpProgress);
            yield return null;
        }

        // Ensure the final rotation is accurate
        targetTransform.rotation = targetRotation;
    }
}
