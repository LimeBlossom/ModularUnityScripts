using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobblePosition : MonoBehaviour
{
    [SerializeField] private GameObject toWobble;

    [SerializeField] private int seed = -1;

    [SerializeField] private MinMaxVector3 wobbleScaleBounds;
    [SerializeField] private Vector3 wobbleScale;
    [SerializeField] private MinMaxVector3 wobbleOffsetBounds;
    [SerializeField] Vector3 wobbleOffset;
    [SerializeField] private MinMaxVector3 wobbleFrequencyBounds;
    [SerializeField] Vector3 wobbleFrequency;

    private float timeAlive = 0;

    [SerializeField] private Vector3 originalPos;
    private Vector3 originalParentPos;

    private void Start()
    {
        timeAlive = Time.realtimeSinceStartup;

        if (toWobble == null)
        {
            toWobble = gameObject;
        }

        wobbleScale = wobbleScaleBounds.Random(seed);
        wobbleOffset = wobbleOffsetBounds.Random(seed);
        wobbleFrequency = wobbleFrequencyBounds.Random(seed);

        originalPos = toWobble.transform.position + wobbleOffset;
        originalParentPos = transform.parent.position;
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;

        Vector3 curWobble;
        curWobble.x = Mathf.Sin(timeAlive * wobbleFrequency.x);
        curWobble.y = Mathf.Sin(timeAlive * wobbleFrequency.y);
        curWobble.z = Mathf.Sin(timeAlive * wobbleFrequency.z);

        Vector3 wobbleVector = Vector3.Cross(wobbleScale, curWobble);
        
        toWobble.transform.position = originalPos + wobbleVector + transform.parent.position - originalParentPos;
    }
}
