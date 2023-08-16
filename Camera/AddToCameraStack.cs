using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AddToCameraStack : MonoBehaviour
{
    [SerializeField] private Camera master;
    [SerializeField] private Camera[] toAdd;

    private void OnEnable()
    {
        if(master == null)
        {
            master = Camera.main;
        }
        foreach(Camera cam in toAdd)
        {
            var cameraData = master.GetUniversalAdditionalCameraData();
            cameraData.cameraStack.Add(cam);
        }
    }

    private void OnDisable()
    {
        if (master == null)
        {
            master = Camera.main;
        }
        foreach (Camera cam in toAdd)
        {
            if(master != null)
            {
                var cameraData = master.GetUniversalAdditionalCameraData();
                cameraData.cameraStack.Remove(cam);
            }
        }
    }
}
