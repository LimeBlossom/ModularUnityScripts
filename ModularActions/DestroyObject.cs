using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DestroyObject : MonoBehaviour, ISettableGameObject, IActivatable
{
    [SerializeField] private GameObject[] toDestroy;
    public void Activate()
    {
        foreach(GameObject obj in toDestroy)
        {
            Destroy(obj);
        }
    }

    public void DestroyObjectActivate()
    {
        Activate();
    }

    public void SetGameObject(GameObject setTo)
    {
        toDestroy = new GameObject[] { setTo };
    }

    public void SetGameObject(GameObject[] setTo)
    {
        toDestroy = setTo;
    }
}
