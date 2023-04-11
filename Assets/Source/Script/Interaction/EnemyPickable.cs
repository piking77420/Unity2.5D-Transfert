using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.DebugUI;

public class EnemyPickable : InteractableObject

{ 
        
    

    // Start is called before the first frame update


    [SerializeField]
    private NavMeshAgent m_NavMeshAgent;

    [SerializeField]
    private EnemyPatrol m_EnemyPatrol;


    [SerializeField]
    private Rigidbody m_Rigidbody;


    [SerializeField]
    private EnemyThrowedBehaviour m_EnemyThrowedBehaviour;



    [SerializeField]
    protected CheckIsGround m_CheckIsGrounded;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_EnemyPatrol = GetComponent<EnemyPatrol>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_CheckIsGrounded = GetComponent<CheckIsGround>();
        m_EnemyThrowedBehaviour= GetComponent<EnemyThrowedBehaviour>(); 

    }




    private void OnThrowing() 
    {
        if (m_EnemyThrowedBehaviour.Is_Throwed && !m_CheckIsGrounded.isGrounded)
        {
            m_Rigidbody.useGravity = true;
            m_EnemyPatrol.enabled = false;
            m_NavMeshAgent.enabled = false;
        }

        if (m_EnemyThrowedBehaviour.Is_Throwed && m_CheckIsGrounded.isGrounded) 
        {
            m_EnemyThrowedBehaviour.Is_Throwed = false;
            m_EnemyPatrol.enabled = true;
            m_NavMeshAgent.enabled = true;
        }
    }


    void Start()
    {

    }

    // Update is called once per frame

    // Need To Chnage it if statement   







    void Update()
    {

        OnThrowing();

    }
}
