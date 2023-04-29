using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyFollowPath : EnemyPatrol
{




    protected new void Awake()
    {
        base.Awake();


        foreach (var item in m_WayPoints)
        {
            item.Set(item.x, gameObject.transform.position.y, item.z);
        }
    }


    void Start()
    {
        
    }

    protected new void HasReachLastPoint() 
    {

        if(m_CurrentWaypoint == m_WayPoints.Count)
        {

            StartCoroutine(EnemyWaitTime(m_WaitTime));
            m_WayPoints.Reverse();
        }
    }



    protected new void FollowPath()  
    {
        m_NavMeshAgent.SetDestination(m_WayPoints[m_CurrentWaypoint]);
    }


    // Update is called once per frame
    protected override void Update()
    {
        if (!m_IsWaiting)
        {
            IsReachingWaypoint();
            HasReachLastPoint();
            FollowPath();

        }

       
    }
}
