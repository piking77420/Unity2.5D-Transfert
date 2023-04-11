using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField]
    protected NavMeshAgent m_NavMeshAgent;
    [SerializeField]
    protected CheckIsGround m_CheckIsGrounded;

    [SerializeField]
    protected Vector3[] m_WayPoints;

    [SerializeField]
    protected int m_CurrentWaypoint;



    [SerializeField]
    protected float m_DistanceToGoNext;



    [SerializeField]
    protected float m_WaitTime;


    [SerializeField]
    protected bool m_IsWaiting;





    [SerializeField]
    private float m_GizmoSize;

    [SerializeField]
    private bool ShowWayPoint;




    private void OnDrawGizmos()
    {
        if (ShowWayPoint)
            for (int i = 0; i < m_WayPoints.Length; i++)
            {
                if (i == m_CurrentWaypoint)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }
                Gizmos.DrawWireSphere(m_WayPoints[i], m_GizmoSize);

            }
    }


    protected IEnumerator EnemyWaitTime(float waitime)
    {
        m_CurrentWaypoint = 0;
        m_IsWaiting = true;
        yield return new WaitForSeconds(waitime);
        m_IsWaiting = false;
    }



    protected void Awake()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_CheckIsGrounded = GetComponent<CheckIsGround>(); 
    }

    protected virtual void IsReachingWaypoint()
    {
        Vector3 currentPos = gameObject.transform.position;
        if (Vector3.Distance(m_WayPoints[m_CurrentWaypoint], currentPos) <= m_DistanceToGoNext)
        {
            m_CurrentWaypoint++;
        }
    }


    protected virtual void HasReachLastPoint()
    {
    }


    protected virtual void FollowPath()
    {
        m_NavMeshAgent.SetDestination(m_WayPoints[m_CurrentWaypoint]);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
   protected virtual void Update()
    {
        
        if (!m_IsWaiting && m_CheckIsGrounded.isGrounded)
        {
            IsReachingWaypoint();
            HasReachLastPoint();
            FollowPath();

        }
    }
}
