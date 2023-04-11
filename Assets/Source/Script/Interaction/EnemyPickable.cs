using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPickable : InteractableObject
{
    [SerializeField]
    private NavMeshAgent m_NavMeshAgent;

    [SerializeField]
    private EnemyPatrol m_EnemyPatrol;


    [SerializeField]
    private Rigidbody m_Rigidbody;


    




    public void EnemyIsPicked() 
    {
        m_EnemyPatrol.enabled = false;
        m_Rigidbody.useGravity = false;
        m_NavMeshAgent.isStopped = true;
    }

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_EnemyPatrol = GetComponent<EnemyPatrol>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {

        m_OnInteraction.AddListener(EnemyIsPicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
