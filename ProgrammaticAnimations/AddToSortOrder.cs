using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToSortOrder : MonoBehaviour
{
    [SerializeField] private MinMaxInt addAmount;
    [SerializeField] private int timesAmount;
    [SerializeField] private SpriteRenderer[] toSort;
    [SerializeField] private GameObject sortChildren;

    //TODO: Add an option to find all SpriteRenderer within given object

    void Start()
    {
        int toAdd = Random.Range(addAmount.min, addAmount.max) * timesAmount;
        foreach(SpriteRenderer spriteRenderer in toSort)
        {
            spriteRenderer.sortingOrder += toAdd;
        }
        if(sortChildren)
        {
            foreach (SpriteRenderer spriteRenderer1 in sortChildren.GetComponentsInChildren<SpriteRenderer>())
            {
                Debug.Log(spriteRenderer1.name);
                spriteRenderer1.sortingOrder += toAdd;
            }
        }
    }
}
