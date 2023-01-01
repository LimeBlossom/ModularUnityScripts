using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTouch : MonoBehaviour
{
    [SerializeField] private TouchPhase phase;

    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;


    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == phase)
                {
                    RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);

                    foreach (RaycastHit2D hit in hits)
                    {
                        if (hit.collider.gameObject == gameObject)
                        {
                            Activate();
                        }
                    }
                }
            }
        }
    }

    void Activate()
    {
        events.Invoke();
        if (actions.Length > 0)
        {
            foreach (IActivatable action in actions)
            {
                action.Activate();
            }
        }
    }
}
