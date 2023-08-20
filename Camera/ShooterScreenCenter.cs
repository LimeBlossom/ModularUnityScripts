using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScreenCenter : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private List<string> tagsToIgnore;
    [SerializeField] private bool moveToHitPos;

    void LateUpdate()
    {
        RaycastHit[] hits =
            Physics.RaycastAll(cam.transform.position, cam.transform.forward);
        
        foreach(RaycastHit hit in hits)
        {
            if (!tagsToIgnore.Contains(hit.transform.tag))
            {
                if(moveToHitPos)
                {
                    transform.position = hit.point;
                    return;
                }
            }
        }
    }
}
