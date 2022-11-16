using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour, ISettableGameObject, IActivatable
{
    [SerializeField] private GameObject toDestroy;
    public void Activate()
    {
        Destroy(toDestroy);
    }

    public void SetGameObject(GameObject setTo)
    {
        toDestroy = setTo;
    }
}
