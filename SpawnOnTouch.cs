using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnTouch : MonoBehaviour
{
    [SerializeField] private GameObject[] toSpawn;
    [SerializeField] private bool spawnAtParentPosition = true;
    [SerializeField] private TouchPhase phase;
    [SerializeField] private bool trackMouseClicks = false;

    void Update()
    {
        if(Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if(touch.phase == phase)
                {
                    RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);

                    foreach (RaycastHit2D hit in hits)
                    {
                        if (hit.collider.gameObject == gameObject)
                        {
                            Spawn();
                        }
                    }
                }
            }
        }
        else if (trackMouseClicks && Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    Spawn();
                }
            }
        }
    }

    private void Spawn()
    {
        foreach (GameObject spawn in toSpawn)
        {
            GameObject spawned = Instantiate(spawn);
            if (spawnAtParentPosition)
            {
                spawned.transform.position = transform.position;
            }
        }
    }
}
