using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructOnCollide : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        // Self-destruct if we hit something with map data
        if(collision.gameObject.GetComponent<MapPieceData>())
        {
            Destroy(gameObject);
        }
    }
}
