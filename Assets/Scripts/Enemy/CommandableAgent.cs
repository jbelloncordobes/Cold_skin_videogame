using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class CommandableAgent : MonoBehaviour
{
    public Transform followTarget;  
    public Transform moveTarget;        
    public NavMeshAgent agent;

    [Header("Follow")]
    public bool isFollowing = false;
    public float repathInterval = 0.2f;    // seconds
    public float followDistance = 2.0f;    // stops this close

    float nextRepathTime;

    [Header("Allowed destinations")]
    public List<Transform> allowedDestinations = new List<Transform>();

    void Reset()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!isFollowing || agent == null || followTarget == null) return;

        // Only repath periodically (cheaper than every frame)
        if (Time.time < nextRepathTime) return;
        nextRepathTime = Time.time + repathInterval;

        float d = Vector3.Distance(transform.position, followTarget.position);
        if (d > followDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(followTarget.position);
        }
        else
        {
            // close enough: stop moving but stay in follow mode
            agent.isStopped = true;
        }
    }

    public void Follow()
    {
        isFollowing = true;
        agent.isStopped = false;
        nextRepathTime = 0f; // force immediate repath
        Debug.Log($"{name}: Follow mode ON");
    }

    public void Hold()
    {
        isFollowing = false;
        if (agent != null) agent.isStopped = true;
        Debug.Log($"{name}: Follow mode OFF (holding)");
    }

    public void MoveToMarker(Transform moveTarget)
    {
        if (moveTarget == null || agent == null) return;
        agent.isStopped = false;
        isFollowing = false;
        agent.SetDestination(moveTarget.position);
        Debug.Log($"{name}: Moving to marker");
    }
}
