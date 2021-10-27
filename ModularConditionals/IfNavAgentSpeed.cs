using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class IfNavAgentSpeed : MonoBehaviour, IActivatable
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private float isGreaterThan = -1;
    [SerializeField] private float isLessThan = -1;
    [SerializeField] private float isEqualTo = -1;

    [SerializeField] private MonoBehaviour[] greaterThanActions;
    [SerializeField] private MonoBehaviour[] lessThanActions;
    [SerializeField] private MonoBehaviour[] equalToActions;

    [SerializeField] private UnityEvent events;

    public void Activate()
    {
        if(isGreaterThan > -1)
        {
            if (agent.velocity.magnitude / agent.speed > isGreaterThan)
            {
                ActivateActions(greaterThanActions);
            }
        }
        if(isLessThan > -1)
        {
            if (agent.velocity.magnitude / agent.speed < isLessThan)
            {
                ActivateActions(lessThanActions);
            }
        }
        if(isEqualTo > -1)
        {
            if (agent.velocity.magnitude / agent.speed == isEqualTo)
            {
                ActivateActions(equalToActions);
            }
        }
    }

    private void ActivateActions(MonoBehaviour[] actions)
    {
        events.Invoke();
        if (actions != null && actions.Length > 0)
        {
            foreach (IActivatable action in actions)
            {
                action.Activate();
            }
        }
    }
}
