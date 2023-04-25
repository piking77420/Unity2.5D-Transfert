using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTryToKillPlayer : MonoBehaviour
{
    // Start is called before the first frame update




    [SerializeField]
    private GameObject m_ListeOfWaypoint;
    private EnemyFollowPlayer m_EnemyFollowPlayer;

    [SerializeField]
    private Transform m_Player;

    private PlayerStatus m_PlayerStatus;

    [SerializeField, Range(0, 10)]
    private float m_DistanceToKillPlayer;

    [SerializeField, Range(0, 10)]
    private float m_VelocityOnKillPlayer;

    [SerializeField]
    private bool m_ShowGizmo;

    [SerializeField]
    private NavMeshAgent m_Agent;

    [SerializeField, Range(0, 10)]
    private float m_EndGameDistance;











    private void ResetPosOnPlayerDeath() 
    {
        int playerIndexCheckPoint = m_PlayerStatus.currentCheckpointIndex;

        Vector3 posToRespawn = new Vector3();

        for (int i = 0; i < m_ListeOfWaypoint.transform.childCount; i++)
        {

            if(i == playerIndexCheckPoint) 
            {
                posToRespawn = m_ListeOfWaypoint.transform.GetChild(i).position;
                break;
            }
        }

        this.transform.position = posToRespawn;

    }



    public void OnKillHitBox() 
    {
        if (m_PlayerStatus.IsDead)
        {
            return;
        }


        Collider[] colliders = Physics.OverlapSphere(transform.position, m_DistanceToKillPlayer);

        foreach (Collider collider in colliders) 
        {


            if(collider.gameObject.transform == m_Player && collider.gameObject.transform.parent.TryGetComponent<DimensionScriptPlayer>(out DimensionScriptPlayer d) )
            {
                if(d.CurrentDimension == DimensionScript.Dimension.Normal) 
                {
                    Ray r = new Ray(transform.position , m_Player.position - this.transform.position);

                    if(Physics.Raycast(r, out RaycastHit hit, m_DistanceToKillPlayer) && hit.collider == m_Player.GetComponent<Collider>()) 
                    {
                        m_PlayerStatus.IsDead = true;
                    }
                }
            }
        }    
    }

    


    private void OnDrawGizmos()
    {


        if (m_ShowGizmo) 
        {
           
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, m_DistanceToKillPlayer);    
        }
        
    }

    private void OnRespawnPlayer() 
    {
        // tranform = last hceckpoint
    }

    private void Awake()
    {
        m_EnemyFollowPlayer = GetComponent<EnemyFollowPlayer>();
        m_Agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        m_PlayerStatus = m_Player.parent.GetComponent<PlayerStatus>();
        m_PlayerStatus.OnPlayerDeath.AddListener(OnRespawnPlayer);
        m_PlayerStatus.OnPlayerDeath.AddListener(ResetPosOnPlayerDeath);
    }

    void Update()
    {

        OnKillHitBox();

    }
}
