using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGraphicUpdate : MonoBehaviour
{

    [Header("Dependencies")]
    [SerializeField]
    private Animator m_PlayerGraphicAnimator;

    [SerializeField]
    private PlayerPushBox m_PlayerPushBox;
    [SerializeField]
    private PlayerInteraction m_PlayerInteraction;

    [SerializeField]
    private PlayerMovement m_PlayerMovement;

    [SerializeField]
    private PlayerInput PlayerInput;

    [SerializeField]
    private Transform m_PlayerPos;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private PlayerJump m_PlayerJump;



    [SerializeField]
    private Quaternion m_LastRoatation;



    [Space(1), Header("Ik Part")]


    [SerializeField]
    private LayerMask m_LayerMask;

    [SerializeField, Range(0, 10)]
    private float DistanceFromGround;

    [SerializeField, Range(0, 10)]
    private float m_WeightFoot;







    private float maxIdleJump;
    private float currentIdleJump = 0;

    public void OnJumping(InputAction.CallbackContext _callbackContext)
    {
        if (_callbackContext.performed)
        {
            m_PlayerGraphicAnimator.SetTrigger("Jump");

        }

    }

    public void OnMoveBox(InputAction.CallbackContext _callbackContext)
    {
        if (_callbackContext.performed)
        {

            //PushBox();
        }

    }





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


    private void IdleJump()
    {
        if (rb.velocity.y < 0 - 0.5)
        {
            if (currentIdleJump <= maxIdleJump)
            {
                currentIdleJump += Time.deltaTime;
                m_PlayerGraphicAnimator.SetFloat("SpeedY", -currentIdleJump);
            }



        }
        else
        {
            currentIdleJump = 0;
            m_PlayerGraphicAnimator.SetFloat("SpeedY", 0);

        }
    }



    private void PushBox()
    {


        bool IsInMoveBoxMap = PlayerInput.currentActionMap.name == "MoveBox";

        if (!IsInMoveBoxMap)
        {
            m_PlayerGraphicAnimator.SetBool("PushingBox", false);
            return;
        }
        else
        {
            m_PlayerGraphicAnimator.SetBool("PushingBox", true);
        }

        if (m_PlayerPushBox.m_CurrentBox != null)
        {
            Vector3 vectorBox = (m_PlayerPos.position - m_PlayerPushBox.m_CurrentBox.transform.position).normalized;


            Vector3 lookatRotation = new Vector3(m_PlayerPushBox.m_CurrentBox.transform.position.x, this.transform.position.y, this.transform.position.z);
            this.transform.LookAt(lookatRotation);


            if (vectorBox.x > 0)
            {

                m_PlayerGraphicAnimator.SetFloat("PushingBoxMovment", (m_PlayerMovement.movement.x));
            }
            else if (vectorBox.x < 0)
            {
                m_PlayerGraphicAnimator.SetFloat("PushingBoxMovment", -(m_PlayerMovement.movement.x));
            }


        }
        else
        {
            m_PlayerGraphicAnimator.SetBool("PushingBox", false);
        }
    }

    private void SetAnimator()
    {

        m_PlayerGraphicAnimator.SetFloat("SpeedX", MathF.Abs(m_PlayerMovement.movement.x));
        IdleJump();
        PushBox();
    }


    private void Awake()
    {
        m_PlayerGraphicAnimator = GetComponent<Animator>();
        m_PlayerMovement = GetComponentInParent<PlayerMovement>();
        m_PlayerJump = GetComponentInParent<PlayerJump>();
        PlayerInput = GetComponentInParent<PlayerInput>();
        m_PlayerPushBox = GetComponentInParent<PlayerPushBox>();
        maxIdleJump = 1f;
        m_PlayerPos = transform.GetChild(0).transform;
        m_PlayerInteraction = GetComponentInParent<PlayerInteraction>();
    }
    void Start()
    {
        rb = m_PlayerMovement.m_Rigidbody;
        Quaternion StartRotation = new Quaternion();
        StartRotation.eulerAngles = new Vector3(0, 90, 0);
        this.transform.rotation = StartRotation;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovment();
    }

    private void UpdateMovment()
    {


        SetAnimator();
        if (m_PlayerPushBox.m_CurrentBox == null)
            UpdateRotation();
        LeverAnimation();
    }

    private void LeverAnimation()
    {

        m_PlayerGraphicAnimator.SetBool("Onlever", m_PlayerInteraction.LeverInteraction);

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
        if (m_PlayerGraphicAnimator)
        {
            LeftFoot();
            RightFoot();

        }
    }
}
