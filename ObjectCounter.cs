using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCounter : MonoBehaviour, IActivatable
{
    [SerializeField] private IntReference count;
    [SerializeField] private string[] tagsToCount;
    [SerializeField] private GameObject[] objectsToLookIn;
    [SerializeField] private bool onUpdate = true;

    public void Activate()
    {
        CountObjects();
    }

    // Update is called once per frame
    void Update()
    {
        if (onUpdate)
        {
            CountObjects();
        }
    }

    private void CountObjects()
    {
        int curCount = 0;
        if(objectsToLookIn.Length <= 0)
        {
            foreach(string tag in tagsToCount)
            {
                curCount += GameObject.FindGameObjectsWithTag(tag).Length;
            }
        }
        count.variable.value = curCount;
    }
}
