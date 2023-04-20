using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyThrowedBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Dependencies")]
    [SerializeField]
    private Animator m_Animator;
    [SerializeField]
    private Rigidbody m_rb;


    [SerializeField]
    public bool Is_Throwed;



    private void OnTriggerEnter()
    {
        if (Is_Throwed) 
        {
            m_Animator.enabled = true;
            m_rb.useGravity = true;
            Is_Throwed = false;
            m_rb.isKinematic = true;
        }
    }





    private void Awake()
    {
        m_Animator = GetComponentInParent<Animator>();
        m_rb = GetComponentInParent<Rigidbody>();
    }

    void Start()
    {
        
    }



    

   
}
