using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtObject : MonoBehaviour, IActivatable, ISettableGameObject
{
    [SerializeField] private GameObject toPoint;
    public GameObject target;
    [SerializeField] private bool threeDimensional;

    [SerializeField] private string toPointAt;

    private void Start()
    {
        if(toPoint == null)
        {
            toPoint = gameObject;
        }
    }

    public void Activate()
    {
        if (target != null)
        {
            if (!threeDimensional)
            {
                Vector3 dir = toPoint.transform.position - target.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
                toPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
    public void SetGameObject(GameObject[] setTo)
    {
        target = setTo[0];
    }
}
