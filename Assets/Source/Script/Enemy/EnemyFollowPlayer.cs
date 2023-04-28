using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowPlayer : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField]
    public Transform playerTransform;
    [SerializeField]
    private DimensionScript m_DimensionScriptPlayer;

    [SerializeField]
    private NavMeshAgent m_Agent;


    [Header("Waypoint On Player Changing Dimension")]
    [SerializeField]
    private List<Vector3> m_WayPointTransforms = new List<Vector3>();
    [SerializeField]
    private List<Vector3> m_CurrentsWaypointTransforms = new List<Vector3>();

    [SerializeField]
    private GameObject m_ListWaypoint;


    [Header("Gizmo")]

  
    [SerializeField]
    private bool m_ShowGizmo;

    [SerializeField]
    private Color m_ColorWaypoints;
    [SerializeField]
    private Color m_ColorSelectedWaypoints;
    [SerializeField]
    private float m_WaypointSize = 2 ;


    private int CurrentWaypointIndex = 0;


    private void GetAllWaypoint() 
    {
        Transform[] points;
        points = m_ListWaypoint.GetComponentsInChildren<Transform>();

        m_WayPointTransforms.Clear();
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i] != m_ListWaypoint.transform)
                m_WayPointTransforms.Add(points[i].position);
        }
    }


    private Vector2 GetPlayerPos() 
    {
        Transform PlayerTransform = m_DimensionScriptPlayer.transform.GetChild(0).transform;


        return new Vector2(PlayerTransform.position.x , PlayerTransform.position.y) ;
    }

   

    public Transform ReturnPlayerTransform() 
    {
        return playerTransform;
    }

    private void OnDrawGizmos()
    {
        GetAllWaypoint();

        if (m_ShowGizmo) 
        {
           



            foreach (Vector3 t in m_WayPointTransforms) 
            {
                for (int i = 0; i < m_CurrentsWaypointTransforms.Count; i++)
                {
                    if(t != m_CurrentsWaypointTransforms[i]) 
                    {
                        Gizmos.color = m_ColorWaypoints;
                    }
                    else 
                    {
                        Gizmos.color = m_ColorSelectedWaypoints;
                    }
                }
                Gizmos.DrawSphere(t, m_WaypointSize);
            }
        }
    }

    private bool IfPlayerIsInSpecialZone() 
    {
        if(m_DimensionScriptPlayer.CurrentDimension == DimensionScript.Dimension.Special) 
        {
            return true;
        }
        return false;
    }

    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        GetAllWaypoint();
    }

    private void Movment() 
    {
        Vector3 PlayerNormalWorldOffset = playerTransform.position;
        PlayerNormalWorldOffset.z = -DimensionScript.DimensionSize / 2f;

        m_Agent.SetDestination(PlayerNormalWorldOffset);
    }


    private List<Vector3> GetNearestPoint() 
    {
        Vector2 PlayerPos = GetPlayerPos();
        List<Vector3> points = new List<Vector3>();
        List<Vector3> pointsLessX = new List<Vector3>();

        for (int i = 0; i < 2; i++)
            points.Add(new Vector3());



        float value1 = float.MaxValue;
        float value2 = float.MaxValue;


        foreach (Vector3 Pos in m_WayPointTransforms)
        {
            if (Pos.x < PlayerPos.x)
            {
                if (Vector2.Distance(Pos, PlayerPos) < value1)
                {

                    value1 = Vector2.Distance(Pos, PlayerPos);
                    points[0] = Pos;

                }
            }
        }


        foreach (Vector3 Pos in m_WayPointTransforms)
        {
            if (Pos.x > PlayerPos.x)
            {
                if (Vector2.Distance(Pos, PlayerPos) < value2)
                {

                    value2 = Vector2.Distance(Pos, PlayerPos);
                    points[1] = Pos;

                }
            }
        }


        return points;
    }


    private void WaitingPlayer()
    {

        m_CurrentsWaypointTransforms = GetNearestPoint();

    


        if (m_Agent.remainingDistance < m_Agent.stoppingDistance)
        {
            if(CurrentWaypointIndex == 0) 
            {
                CurrentWaypointIndex = 1;
            }
            else 
            {
               
               CurrentWaypointIndex = 0;
               
            }
        }
        Vector3 posToGo = m_CurrentsWaypointTransforms[CurrentWaypointIndex];
        posToGo.z = -DimensionScript.DimensionSize / 2f;
        m_Agent.SetDestination(posToGo);

    }

      


    void Update()
    {
        if (IfPlayerIsInSpecialZone()) 
        {
            WaitingPlayer();
        }
        else 
        {
            Movment();
        }


    }
}
