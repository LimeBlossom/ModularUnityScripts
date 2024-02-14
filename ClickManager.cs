using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    void Update()
    {
        List<Vector2> positions = new List<Vector2>();

        if(Input.touchCount > 0)
        {
            for(int i = 0; i < Input.touchCount; i++)
            {
                if(Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    positions.Add(Input.GetTouch(i).position);
                }
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            positions.Add(Input.mousePosition);
        }

        foreach(Vector2 position in positions)
        {
            Ray ray = Camera.main.ScreenPointToRay(position);
            RaycastHit[] hits = Physics.RaycastAll(ray, 100);
            System.Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));

            foreach (RaycastHit hit in hits)
            {
                if(hit.transform.GetComponent<ClickManagerIgnore>())
                {
                    continue;
                }
                IClickable clickable = hit.transform.gameObject.GetComponent<IClickable>();

                if (clickable != null)
                {
                    clickable.Click();
                    break;
                }
            }
        }
    }
}
