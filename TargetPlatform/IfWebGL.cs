using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IfWebGL : MonoBehaviour, IActivatable
{
    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    [SerializeField] private bool onStart;
    [SerializeField] private bool onUpdate;

    void Start()
    {
        if (onStart)
            Activate();
    }

    void Update()
    {
        if (onUpdate)
            Activate();
    }

    private void ActivateActions()
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

    public void Activate()
    {
#if PLATFORM_WEBGL
        ActivateActions();
#endif
    }
}
