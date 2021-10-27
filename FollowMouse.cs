using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private void Awake()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); ;
        transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); ;
        transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }
}
