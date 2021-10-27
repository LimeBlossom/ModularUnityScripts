using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GetNavMeshSpeed : MonoBehaviour, IActivatable
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private MonoBehaviour[] floatSettables;
    [SerializeField] private MonoBehaviour[] actions;

    // Returns a value between 0 and 1
    public void Activate()
    {
        float speed = agent.velocity.magnitude / agent.speed;
        ActivateActions(speed);
    }

    private void ActivateActions(float speed)
    {
        // Call settables before actions
        if (floatSettables != null && floatSettables.Length > 0)
        {
            foreach (ISettableFloat settable in floatSettables)
            {
                settable.SetFloat(speed);
            }
        }
        if (actions != null && actions.Length > 0)
        {
            foreach (IActivatable action in actions)
            {
                action.Activate();
            }
        }
    }
}
