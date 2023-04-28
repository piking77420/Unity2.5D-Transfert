using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyThrowedBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    private enum SlimeSound 
    {
        BeTaken,
        Throwed
    }


    [Header("Dependencies")]
    [SerializeField]
    private Animator m_Animator;
    [SerializeField]
    private Rigidbody m_rb;


    [SerializeField]
    public bool Is_Throwed;



    [SerializeField,Tooltip("Timer for destroy enemie while staying throwed for too long"),Range(0,10)]
    private float TimerForEnemyDie;

    
    private float m_MaxTimerForEnemyDie;

    /*

    [Header("AudioPart")]
    [SerializeField]
    private AudioSource m_AudioSource;

    private AudioClip[] m_AudioClip;

    */
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
        m_MaxTimerForEnemyDie = TimerForEnemyDie;
       // m_AudioClip = GetComponents<AudioClip>();
    }

    void Start()
    {
        
    }



    private void Update()
    {
        if (Is_Throwed) 
        {
            TimerForEnemyDie -= Time.deltaTime;

            if(TimerForEnemyDie <= 0) 
            {
                Destroy(transform.root.gameObject);
            }
        }
    }


}
