using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public bool fade;

    public bool dieFromDistance;
    public bool dieFromDistanceX;
    public bool dieFromDistanceY;

    public float maxXPos;
    public bool positiveXOnly;
    public bool negativeXOnly;

    public float maxYPos;
    public bool positiveYOnly;
    public bool negativeYOnly;

    public GameObject[] spawnOnDeath;
    public bool spawnAtPosition;

    [System.Serializable]
    public struct Life { public float min; public float max; }
    public Life m_life;

    private Life cur_life;

    void Start()
    {
        cur_life.max = m_life.max;
        cur_life.min = Random.Range(m_life.min, m_life.max);
        StartCoroutine(StartDying());
    }

    private void Update()
    {
        if(dieFromDistanceX)
        {
            if (Mathf.Abs(transform.position.x) > maxXPos)
            {
                if (positiveXOnly)
                {
                    if (transform.position.x > 0)
                    {
                        DestroySelf();
                    }
                }
                else if (negativeXOnly)
                {
                    if (transform.position.x < 0)
                    {
                        DestroySelf();
                    }
                }
                else
                {
                    DestroySelf();
                }
            }
        }
        if(dieFromDistanceY)
        {
            if (Mathf.Abs(transform.position.y) > maxYPos)
            {
                if (positiveYOnly)
                {
                    if (transform.position.y > 0)
                    {
                        DestroySelf();
                    }
                }
                else if (negativeYOnly)
                {
                    if (transform.position.y < 0)
                    {
                        DestroySelf();
                    }
                }
                else
                {
                    DestroySelf();
                }
            }
        }
        if(fade && !dieFromDistance)
        {
            cur_life.min -= Time.deltaTime * Time.timeScale;
            Color tempCol = GetComponentInChildren<SpriteRenderer>().color;
            GetComponentInChildren<SpriteRenderer>().color = new Color(tempCol.r, tempCol.g, tempCol.b, 5 * cur_life.min / cur_life.max);
        }
    }

    private void DestroySelf()
    {
        if(spawnOnDeath != null)
        {
            if (spawnOnDeath.Length > 0)
            {
                foreach (GameObject toSpawn in spawnOnDeath)
                {
                    GameObject spawned = Instantiate(toSpawn);
                    if (spawnAtPosition)
                    {
                        spawned.transform.position = transform.position;
                    }
                }
            }
        }
        Destroy(gameObject);
    }

    IEnumerator StartDying()
    {
        yield return new WaitForSeconds(cur_life.min);
        DestroySelf();
    }
}
