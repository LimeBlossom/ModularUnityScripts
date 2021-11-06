using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    [SerializeField] private GameObject toWobble;

    public bool wobbleSize;
    public bool wobblePosition;
    public bool wobbleRotation;
    public bool basedOnXPos;

    public Vector3 wobbleScale;
    public Vector3 wobbleOffset;
    public float wobbleFrequency = 1;

    private float timeAlive = 0;

    private Vector3 originalPos;

    private void Start()
    {
        if(toWobble == null)
        {
            toWobble = gameObject;
        }

        originalPos = toWobble.transform.position + wobbleOffset;
    }

    private void OnEnable()
    {
        originalPos = toWobble.transform.position + wobbleOffset;
    }


    void Update()
    {
        timeAlive += Time.deltaTime;

        float curWobble = Mathf.Sin(timeAlive * wobbleFrequency);
        if (basedOnXPos)
        {
            curWobble = Mathf.Sin(wobbleFrequency * (toWobble.transform.position.x + wobbleOffset.x));
        }
        
        if(wobblePosition)
        {
            Vector3 wobbleVector = new Vector3(wobbleScale.x * curWobble + wobbleOffset.x, wobbleScale.y * curWobble + wobbleOffset.y, wobbleScale.z * curWobble + wobbleOffset.z);
            toWobble.transform.position = originalPos + wobbleVector;
        }
        if(wobbleSize)
        {
            Vector3 temp = new Vector3(wobbleScale.x * curWobble + wobbleOffset.x, wobbleScale.y * curWobble + wobbleOffset.y, wobbleScale.z * curWobble + wobbleOffset.z);
            toWobble.transform.localScale = temp;
        }
        if(wobbleRotation)
        {
            Vector3 temp = new Vector3(wobbleScale.x * curWobble + wobbleOffset.x, wobbleScale.y * curWobble + wobbleOffset.y, wobbleScale.z * curWobble + wobbleOffset.z);
            toWobble.transform.localRotation = Quaternion.Euler(temp);
        }
    }
}
