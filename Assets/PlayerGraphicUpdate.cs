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
    private LayerMask m_LayerMask;


    [SerializeField]

    private Quaternion m_LastRoatation;

    [SerializeField, Range(0, 10)]
    private float DistanceFromGround;


    [SerializeField, Range(0, 10)]
    private float m_WeightFoot;








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



    private void LeftFoot() 
    {
        
            m_PlayerGraphicAnimator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, m_WeightFoot);
            m_PlayerGraphicAnimator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, m_WeightFoot);



            RaycastHit hit;

            Ray ray = new Ray(m_PlayerGraphicAnimator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);

            if (Physics.Raycast(ray, out hit, DistanceFromGround + 1f, m_LayerMask))
            {
                Vector3 footPos = hit.point;
                footPos.y += DistanceFromGround;
                m_PlayerGraphicAnimator.SetIKPosition(AvatarIKGoal.LeftFoot, footPos);
            }
    }

    private void RightFoot()
    {
        m_PlayerGraphicAnimator.SetIKPositionWeight(AvatarIKGoal.RightFoot, m_WeightFoot);
        m_PlayerGraphicAnimator.SetIKRotationWeight(AvatarIKGoal.RightFoot, m_WeightFoot);


        RaycastHit hit;

        Ray ray = new Ray(m_PlayerGraphicAnimator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);

        if (Physics.Raycast(ray, out hit, DistanceFromGround + 1f, m_LayerMask))
        {
            Vector3 footPos = hit.point;
            footPos.y += DistanceFromGround;
            m_PlayerGraphicAnimator.SetIKPosition(AvatarIKGoal.RightFoot, footPos);

        }
    }


        private void OnAnimatorIK()
        {


            if (m_PlayerGraphicAnimator )
            {
                LeftFoot();
                RightFoot();
            }
        }
}
