using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekAndDestroy : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject[] objectToDestroy;
    [SerializeField] private string[] objectTagToDestroy;
    [SerializeField] private bool canDestroySelf = false;
    private bool debug = true;

    public void Activate()
    {
        if (debug)
        {
            print($"SeekAndDestroy of {name} activated.");
        }
        DestroyGO(objectToDestroy);
        foreach(string tempTag in objectTagToDestroy)
        {
            DestroyGO(GameObject.FindGameObjectsWithTag(tempTag));
        }
    }

    private void DestroyGO(GameObject[] toDestroy)
    {
        foreach (GameObject go in toDestroy)
        {
            if (debug)
            {
                print($"{name} is destroying {go.name}");
            }
            DestroyGO(go);
        }
    }

    private void DestroyGO(GameObject toDestroy)
    {
        if (toDestroy == gameObject && !canDestroySelf)
        {
            if (debug)
            {
                print($"{name} will not destroy itself.");
            }
            return;
        }

        if (debug)
        {
            print($"{name} is destroying {toDestroy.name}");
        }

        Destroy(toDestroy);
    }
}
