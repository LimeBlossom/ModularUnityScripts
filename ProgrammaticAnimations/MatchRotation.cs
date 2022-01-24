using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchRotation : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject matcher;
    [SerializeField] private GameObject toMatch;

    public void Activate()
    {
        matcher.transform.forward = toMatch.transform.forward;
    }
}
