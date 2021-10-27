using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSize : MonoBehaviour
{
    [System.Serializable]
    public struct SizeRange { public float min; public float max; }
    public SizeRange m_sizeRange;

    void Start()
    {
        transform.localScale = transform.localScale * Random.Range(m_sizeRange.min, m_sizeRange.max);
    }
}
