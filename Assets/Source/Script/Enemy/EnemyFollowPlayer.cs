using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowPlayer : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField]
    private Transform m_PlayerTransform;


    [SerializeField]
    private NavMeshAgent m_Agent;


    [Header("Waypoint On Player Changing Dimension")]
    [SerializeField]
    private List<Transform> transforms = new List<Transform>();






    [Header("Gizmo")]

    [SerializeField]
    private Vector3 BoxSearchSize;
    [SerializeField]
    private bool m_ShowGizmo;
    [SerializeField]
    private Color m_ColorOfGizmo;







    private void OnDrawGizmos()
    {
        if (m_ShowGizmo) 
        {
            Gizmos.color = m_ColorOfGizmo;
            Gizmos.DrawWireCube(this.transform.position, BoxSearchSize);
        }
    }

    /*
    private bool EnemySeekForPlayer()
    {
        Collider[] SeekingBox  =  Physics.OverlapBox(this.transform.position, BoxSearchSize);

        foreach (Collider c in SeekingBox) 
        {

        }

    }*/




    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void VectorMovment() 
    {
        Vector3 PlayerNormalWorldOffset = m_PlayerTransform.position;
        PlayerNormalWorldOffset.z = -DimensionScript.DimensionSize / 2f;
       // Debug.Log(PlayerNormalWorldOffset);

        m_Agent.SetDestination(PlayerNormalWorldOffset);
    }

    // Update is called once per frame
    void Update()
    {

        VectorMovment();

    }
}
