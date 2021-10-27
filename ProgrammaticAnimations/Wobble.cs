using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    public bool wobbleSize;
    public bool wobblePosition;
    public bool wobbleRotation;
    public bool basedOnXPos;

    public Vector3 wobbleScale;
    public Vector3 wobbleOffset;
    public float wobbleFrequency = 1;

    private float timeAlive = 0;

    void Update()
    {
        timeAlive += Time.deltaTime;

        float curWobble = Mathf.Sin(timeAlive * wobbleFrequency);
        if (basedOnXPos)
        {
            curWobble = Mathf.Sin(wobbleFrequency * (transform.position.x + wobbleOffset.x));
        }
        
        if(wobblePosition)
        {
            Vector3 wobbleVector = new Vector3(wobbleScale.x * curWobble, wobbleScale.y * curWobble, wobbleScale.z * curWobble);
            transform.position = transform.position + wobbleVector * Time.deltaTime;
        }
        if(wobbleSize)
        {
            Vector3 temp = new Vector3(wobbleScale.x * curWobble + wobbleOffset.x, wobbleScale.y * curWobble + wobbleOffset.y, wobbleScale.z * curWobble + wobbleOffset.z);
            transform.localScale = temp;
        }
        if(wobbleRotation)
        {
            Vector3 temp = new Vector3(wobbleScale.x * curWobble + wobbleOffset.x, wobbleScale.y * curWobble + wobbleOffset.y, wobbleScale.z * curWobble + wobbleOffset.z);
            transform.localRotation = Quaternion.Euler(temp);
        }
    }
}
