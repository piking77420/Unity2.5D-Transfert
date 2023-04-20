using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerGraphicUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Animator m_PlayerGraphicAnimator;


    [SerializeField]
       private PlayerMovement m_PlayerMovement;



    [SerializeField]

    private Quaternion m_LastRoatation;

    private void UpdateRotation() 
    {
        Vector2 movement = m_PlayerMovement.movement;
        Quaternion rotation = new Quaternion();


        if (movement == Vector2.zero)
        {
            this.transform.rotation = m_LastRoatation;
            return;
        }
    

        if (movement.x < 0) 
        {
            rotation.eulerAngles = new Vector3(0, -90, 0);
            m_LastRoatation = rotation;
            m_LastRoatation = this.transform.rotation;

        }
        else 
        {
            rotation.eulerAngles = new Vector3(0, 90, 0);
            m_LastRoatation = rotation;

        }




        this.transform.rotation = rotation;

        m_LastRoatation = this.transform.rotation;


    }

    private void SetAnimator()
    {
        m_PlayerGraphicAnimator.SetFloat("SpeedX", MathF.Abs(m_PlayerMovement.movement.x));
        m_PlayerGraphicAnimator.SetFloat("SpeedY", m_PlayerMovement.movement.y);

    }


    private void Awake()
    {
        m_PlayerGraphicAnimator = GetComponent<Animator>();
        m_PlayerMovement = GetComponentInParent<PlayerMovement>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovment();

    }

    private void UpdateMovment()
    {
        SetAnimator();
        UpdateRotation();
    }
}
