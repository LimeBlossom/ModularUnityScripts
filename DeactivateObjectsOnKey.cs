using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateObjectsOnKey : MonoBehaviour
{
    public GameObject[] targets;
    public string actionKey;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(actionKey))
        {
            foreach (GameObject target in targets)
            {
                target.SetActive(false);
            }
        }
    }
}
