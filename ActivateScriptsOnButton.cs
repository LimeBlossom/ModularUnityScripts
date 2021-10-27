using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScriptsOnButton : MonoBehaviour
{
    public MonoBehaviour[] scripts;
    public string spawnKey;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            foreach(MonoBehaviour script in scripts)
            {
                script.enabled = true;
            }
        }
    }
}
