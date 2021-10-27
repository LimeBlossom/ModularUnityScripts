using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtObject : MonoBehaviour, IActivatable, ISettableGameObject
{
    public GameObject target;
    [SerializeField] private bool threeDimensional;

    [SerializeField] private string toPointAt;

    public void Activate()
    {
        if (target != null)
        {
            if (!threeDimensional)
            {
                Vector3 dir = transform.position - target.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                transform.LookAt(target.transform);
            }
        }
        else
        {
            target = GameObject.Find(toPointAt);
        }
    }

    public void SetGameObject(GameObject setTo)
    {
        target = setTo;
    }
}
