using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    protected Animator m_Animator;
    
    [SerializeField]
    protected NavMeshAgent m_NavMeshAgent;

    [SerializeField]
    protected CheckIsGround m_CheckIsGrounded;

    [SerializeField]
    protected List<Vector3> m_WayPoints;

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





    public void SetPath(List<Vector3> GivedPath,MobSpawner mobSpawner) 
    {
        for (int i = 0; i < GivedPath.Count; i++)
        {
            m_WayPoints[i] = GivedPath[i] + mobSpawner.transform.position;
        }

        
    }


    private void OnDrawGizmos()
    {
        if (ShowWayPoint)
            for (int i = 0; i < m_WayPoints.Count; i++)
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
        m_Animator.SetBool("IsWaiting", true);
        yield return new WaitForSeconds(waitime);
        m_Animator.SetBool("IsWaiting", false);
        m_IsWaiting = false;
    }



    protected virtual void Awake()
    {
        m_Animator = GetComponent<Animator>();
        if(m_Animator == null) 
        {
            m_Animator = gameObject.GetComponentInParent<Animator>();
        }

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

    public void OnChangingDimension() 
    {
        Debug.Log("before");
        for (int i = 0; i < m_WayPoints.Count; i++)
        {
            Debug.Log(m_WayPoints[i]);

        }

        for (int i = 0; i < m_WayPoints.Count; i++)
        {
            m_WayPoints[i].Set(m_WayPoints[i].x, m_WayPoints[i].y, -m_WayPoints[i].z);
        }

        Debug.Log("Atfer");
        for (int i = 0; i < m_WayPoints.Count; i++)
        {
            Debug.Log(m_WayPoints[i]);

        }
    }

    // Update is called once per frame
   protected virtual void Update()
    {
        
        if (!m_IsWaiting)
        {
            IsReachingWaypoint();
            HasReachLastPoint();
            FollowPath();

        }
    }
}
