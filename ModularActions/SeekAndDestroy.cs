using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekAndDestroy : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject[] goToDestroy;
    [SerializeField] private string[] goTagToDestroy;
    [SerializeField] private bool canDestroySelf = false;

    public void Activate()
    {
        DestroyGO(goToDestroy);
        foreach(string tempTag in goTagToDestroy)
        {
            DestroyGO(GameObject.FindGameObjectsWithTag(tempTag));
        }
    }

    private void DestroyGO(GameObject[] toDestroy)
    {
        foreach(GameObject go in toDestroy)
        {
            DestroyGO(go);
        }
    }

    private void DestroyGO(GameObject toDestroy)
    {
        if (toDestroy == gameObject && !canDestroySelf)
        {
            return;
        }

        Destroy(toDestroy);
    }
}
