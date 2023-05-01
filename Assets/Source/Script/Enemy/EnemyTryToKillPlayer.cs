using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTryToKillPlayer : MonoBehaviour
{
    // Start is called before the first frame update




    private EnemyFollowPlayer m_EnemyFollowPlayer;

    [SerializeField]
    private Transform m_Player;
    [SerializeField]
    private PlayerStatus m_PlayerStatus;

    [SerializeField, Range(0, 10)]
    private float m_DistanceToKillPlayer;

    [SerializeField, Range(0, 10)]
    private float m_VelocityOnKillPlayer;

    [SerializeField]
    private bool m_ShowGizmo;

    [SerializeField]
    private NavMeshAgent m_Agent;






    [SerializeField]
    private float TimeToBeFreez;

    [SerializeField]
    private float m_TimeToChanPosAfterPlayerDeath = 2;





   public IEnumerator RespawnEnemy() 
    {
       

        yield return new WaitForSeconds(m_TimeToChanPosAfterPlayerDeath);
        this.transform.position = m_PlayerStatus.EnemyRespownCheckpoint;
    }



    public void ResetPosOnPlayerDeath() 
    {
        if(m_EnemyFollowPlayer.playerTransform != null)
        StartCoroutine(RespawnEnemy());
    }



    public void OnKillHitBox() 
    {
        if (m_PlayerStatus.IsDead || m_Agent.isStopped)
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
                        m_PlayerStatus.KillPlayer(PlayerStatus.PlayerDiedSource.FromEnemy);
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


    public IEnumerator StopEnemy() 
    {
        m_Agent.isStopped = true;
        yield return new WaitForSeconds(TimeToBeFreez);
        m_Agent.isStopped = false;

    }

    public void Freeze() 
    {
        StartCoroutine(StopEnemy());
    }


    private void Awake()
    {
        m_PlayerStatus = FindObjectOfType<PlayerStatus>();
        m_EnemyFollowPlayer = GetComponent<EnemyFollowPlayer>();
        m_Agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        m_PlayerStatus = m_Player.parent.GetComponent<PlayerStatus>();
        m_PlayerStatus.OnPlayerDeath.AddListener(ResetPosOnPlayerDeath);
        m_PlayerStatus.OnRespawn.AddListener(Freeze);
    }

    void Update()
    {
        if(m_EnemyFollowPlayer.playerTransform!= null)
        OnKillHitBox();

    }
}
