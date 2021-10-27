using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUpThenDie : MonoBehaviour
{
    [System.Serializable]
    public struct Life { public float min; public float max; }
    public Life m_life;

    [System.Serializable]
    public struct Speed { public float min; public float max; }
    public Speed m_speed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Die());
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * Random.Range(m_speed.min, m_speed.max));
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(Random.Range(m_life.min, m_life.max));
        Destroy(transform.gameObject);
    }
}
