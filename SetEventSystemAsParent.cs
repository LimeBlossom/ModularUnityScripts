using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetEventSystemAsParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(FindObjectOfType<EventSystem>().transform);
    }
}
