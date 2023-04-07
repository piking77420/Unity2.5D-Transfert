using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyFollowPath : EnemyPatrol
{
    // Start is called before the first frame update





  


    protected new void Awake()
    {
        base.Awake();
    }


    void Start()
    {
        
    }

    protected new void HasReachLastPoint() 
    {

        if(m_CurrentWaypoint == m_WayPoints.Length)
        {
            StartCoroutine(EnemyWaitTime(m_WaitTime));
            System.Array.Reverse(m_WayPoints);
        }
    }



    protected new void FollowPath()  
    {
        m_NavMeshAgent.SetDestination(m_WayPoints[m_CurrentWaypoint]);
    }


    // Update is called once per frame
    void Update()
    {

        if (!m_IsWaiting)
        {
            IsReachingWaypoint();
            HasReachLastPoint();
            FollowPath();

        }
    }
}
