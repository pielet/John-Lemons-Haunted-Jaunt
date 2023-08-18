using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;

    int m_CurrentWaypointIndex = 0;
    bool m_Forward = true;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            if (m_Forward)
            {
                ++m_CurrentWaypointIndex;
                if (m_CurrentWaypointIndex == waypoints.Length)
                {
                    m_Forward = false;
                    m_CurrentWaypointIndex -= 2;
                }
            }
            else
            {
                --m_CurrentWaypointIndex;
                if (m_CurrentWaypointIndex < 0)
                {
                    m_Forward = true;
                    m_CurrentWaypointIndex += 2;
                }
            }

            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }
}
