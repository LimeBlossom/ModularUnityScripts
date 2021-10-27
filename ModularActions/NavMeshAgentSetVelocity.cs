using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentSetVelocity : MonoBehaviour, IActivatable
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Vector3 setVelocity;

    public void Activate()
    {
        agent.velocity = setVelocity;
    }
}
