using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeRotation : MonoBehaviour
{
    [System.Serializable]
    public struct RotationRange { public Vector3 min; public Vector3 max; }
    public RotationRange m_rotationRange;

    [SerializeField] private float multiplier = 1;
    [SerializeField] private bool castToInt = false;

    void Start()
    {
        float xAngle = Random.Range((float)m_rotationRange.min.x, (float)m_rotationRange.max.x);
        float yAngle = Random.Range((float)m_rotationRange.min.y, (float)m_rotationRange.max.y);
        float zAngle = Random.Range((float)m_rotationRange.min.z, (float)m_rotationRange.max.z);

        if(castToInt)
        {
            xAngle = (int)xAngle;
            yAngle = (int)yAngle;
            zAngle = (int)zAngle;
        }

        transform.eulerAngles = new Vector3(
            xAngle * multiplier,
            yAngle * multiplier,
            zAngle * multiplier);
    }
}
