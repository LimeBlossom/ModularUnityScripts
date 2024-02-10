using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour, IActivatable, ISettableGameObject
{
    [SerializeField] private Transform target;
    [SerializeField] private string tagToTarget;
    [SerializeField] private Transform looker;
    [SerializeField] private bool onUpdate = false;

    void Update()
    {
        if(onUpdate)
        {
            Activate();
        }
    }

    public void Activate()
    {
        if(target == null)
        {
            var targetGO = GameObject.FindGameObjectWithTag(tagToTarget).transform;
            if(targetGO != null)
            {
                target = targetGO.transform;
            }
        }
        if(looker == null)
        {
            looker = transform;
        }
        if(target != null)
        {
            looker.LookAt(target);
        }
    }

    public void SetGameObject(GameObject setTo)
    {
        target = setTo.transform;
    }

    public void SetGameObject(GameObject[] setTo)
    {
        SetGameObject(setTo[0]);
    }
}
