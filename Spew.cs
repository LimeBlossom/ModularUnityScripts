using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spew : MonoBehaviour
{
    public int seed = -1;

    public bool alwaysOn;
    public bool onAwake = false;
    public bool adjustForLag = false;
    public bool onMessage;

    public string messageType;

    public bool attachToParent;
    public GameObject attachToObject;

    private int spewers = 0;

    private System.Random random;

    public GameObject[] toSpew;
    [System.Serializable]
    public struct TimeToSpew { public float min; public float max; }
    public TimeToSpew m_timeToSpew;
    public bool roundTimeToInt = false;
    public bool roundTimeToQuarter = false;
    [System.Serializable]
    public struct NumberToSpew { public int min; public int max; } 
    public NumberToSpew m_numberToSpew;
    [System.Serializable]
    public struct LocalPositionRange { public Vector3 min; public Vector3 max; }
    public LocalPositionRange m_localPositionRange;

    void OnEnable()
    {
        if (onMessage)
        {
            MessageCenter.OnMessage += CheckMessage;
        }
    }

    void OnDisable()
    {
        if (onMessage)
        {
            MessageCenter.OnMessage -= CheckMessage;
        }
    }

    private void Awake()
    {
        if(seed != -1)
        {
            random = new System.Random(seed);
        }
        else
        {
            random = new System.Random(Random.Range(0, 999999));
        }
        if(onAwake)
        {
            StartSpew(random.Next(m_numberToSpew.min, m_numberToSpew.max));
        }
    }

    private void Update()
    {
        if(alwaysOn && spewers == 0)
        {
            StartSpew();
        }
    }

    public void CheckMessage(string type, string message)
    {
        if(type == this.messageType)
        {
            StartSpew(random.Next(m_numberToSpew.min, m_numberToSpew.max)); //Random.Range(m_numberToSpew.min, m_numberToSpew.max));
        }
    }

    public void StartSpew(string unused)
    {
        StartSpew();
    }

    public void StartSpew(int amount)
    {
        int spewNumber = amount;
        for (int i = 0; i < spewNumber; i++)
        {
            spewers++;
            StartCoroutine(SpawnAfterTime());
        }
    }

    public void StartSpew()
    {
        int spewNumber = random.Next(m_numberToSpew.min, m_numberToSpew.max);
        for(int i = 0; i < spewNumber; i++)
        {
            spewers++;
            StartCoroutine(SpawnAfterTime());
        }
    }
    
    IEnumerator SpawnAfterTime()
    {
        float waitTime = (float)random.NextDouble() * (m_timeToSpew.max - m_timeToSpew.min) + m_timeToSpew.min;
        if(roundTimeToInt)
        {
            waitTime = Mathf.Round(waitTime);
        }
        if(roundTimeToQuarter)
        {
            waitTime = Mathf.Round(waitTime * 4) / 4;
        }
        yield return new WaitForSeconds(waitTime);

        if(adjustForLag && Time.deltaTime > 0.02f) // Don't spawn if laggy
        {
            StartCoroutine(SpawnAfterTime());
        }
        else
        {
            DoSpew();
            spewers--;
        }
    }

    private void DoSpew()
    {
        if (attachToParent)
        {
            GameObject spewed = Instantiate(toSpew[random.Next(0, toSpew.Length)], this.transform) as GameObject;
            spewed.transform.position = new Vector3(
                transform.position.x + (float)random.NextDouble() * (m_localPositionRange.max.x - m_localPositionRange.min.x) + m_localPositionRange.min.x, //(m_localPositionRange.min.x, m_localPositionRange.max.x),
                transform.position.y + (float)random.NextDouble() * (m_localPositionRange.max.y - m_localPositionRange.min.y) + m_localPositionRange.min.y, // random.Next(m_localPositionRange.min.y, m_localPositionRange.max.y),
                transform.position.z + (float)random.NextDouble() * (m_localPositionRange.max.z - m_localPositionRange.min.z) + m_localPositionRange.min.z);
        }
        else if (attachToObject != null)
        {
            GameObject spewed = Instantiate(toSpew[random.Next(0, toSpew.Length)], attachToObject.transform) as GameObject;
            spewed.transform.position = new Vector3(
                transform.position.x + (float)random.NextDouble() * (m_localPositionRange.max.x - m_localPositionRange.min.x) + m_localPositionRange.min.x, //(m_localpositionrange.min.x, m_localpositionrange.max.x),
                transform.position.y + (float)random.NextDouble() * (m_localPositionRange.max.y - m_localPositionRange.min.y) + m_localPositionRange.min.y, // random.next(m_localpositionrange.min.y, m_localpositionrange.max.y),
                transform.position.z + (float)random.NextDouble() * (m_localPositionRange.max.z - m_localPositionRange.min.z) + m_localPositionRange.min.z);
        }
        else
        {
            GameObject spewed = Instantiate(toSpew[random.Next(0, toSpew.Length)]) as GameObject;
            spewed.transform.rotation = transform.rotation;
            spewed.transform.position = new Vector3(
                transform.position.x + (float)random.NextDouble() * (m_localPositionRange.max.x - m_localPositionRange.min.x) + m_localPositionRange.min.x, // Random.Range(m_localPositionRange.min.x, m_localPositionRange.max.x),
                transform.position.y + (float)random.NextDouble() * (m_localPositionRange.max.y - m_localPositionRange.min.y) + m_localPositionRange.min.y, // Random.Range(m_localPositionRange.min.y, m_localPositionRange.max.y),
                transform.position.z + (float)random.NextDouble() * (m_localPositionRange.max.z - m_localPositionRange.min.z) + m_localPositionRange.min.z);
        }
    }
}
