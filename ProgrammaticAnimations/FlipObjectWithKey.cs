using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipObjectWithKey : MonoBehaviour
{
    public string[] flipTrue;
    public string[] flipFalse;

    private void Update()
    {
        foreach (string input in flipTrue)
        {
            if (Input.GetKey(input))
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        foreach (string input in flipFalse)
        {
            if (Input.GetKey(input))
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }
}
