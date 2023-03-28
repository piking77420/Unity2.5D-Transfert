using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerJump : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Rigidbody m_Rigidbody;

    [SerializeField]
    private PlayerMovement m_PlayerMovement;


    [Header("JumpValues")]

    [SerializeField]
    private float m_JumpStrengt;


    [Tooltip("Falling acceleration after he start falling")]
    [SerializeField, Range(0, 180)]
    private float fallAcceleration;


    [Tooltip("MaximumValue for falling Velocity")]
    [SerializeField, Range(0, 180)]
    private float FallClampValue;


    [SerializeField, Range(0, 180)]
    private float DesiredAngle;

    [SerializeField, Range(0, 10)]
    private float CoyoteTime;


    [SerializeField]
    private bool m_IsGrounded = false;




    private bool m_Exit;
    private float m_MaxCoyoteTime;



    public void OnJumping(InputAction.CallbackContext _callbackContext)
    {
        if (m_IsGrounded)
        {
            m_Rigidbody.velocity = Vector3.up * m_JumpStrengt;
        }
        m_IsGrounded = false;

    }

  

    bool CheckCollsion(Collision collision)
    {
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            float getangle = Vector3.Angle(collision.contacts[i].normal, Vector3.up);



            if (getangle < DesiredAngle)
            {
                return true;
            }
        }

        return true;

    }

    private void OnCollisionExit(Collision collision)
    {

        if (m_IsGrounded)
        {

            m_Exit = true;
          

        }
        else
        {
            m_Exit = false;
            CoyoteTime = m_MaxCoyoteTime;
        }


    }



    private void OnCollisionEnter(Collision collision)
    {
       

        if (!m_IsGrounded) 
        {
            m_IsGrounded = CheckCollsion(collision);
        }
    }


    private void Awake()
    {
        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_MaxCoyoteTime = CoyoteTime;
    }

    void Start()
    {
        
    }



    void FallClamping() 
    {
        if (m_Rigidbody.velocity.y < 0)
        {
            if (m_Rigidbody.velocity.y < -FallClampValue && !m_IsGrounded)
            {
                m_Rigidbody.velocity = new Vector3(m_PlayerMovement.movement.x, -FallClampValue, m_PlayerMovement.movement.y);
            }
            else
            {
                m_Rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallAcceleration - 1) * Time.deltaTime;
            }
        }
    }


    void InCoyoteTime() 
    {
        if (m_Exit) 
        {
            CoyoteTime -= Time.deltaTime;
            if (CoyoteTime <= 0)
            {
                m_IsGrounded = false;
                m_Exit = false;
                CoyoteTime = m_MaxCoyoteTime;
            }
        }

        
    }


    // Update is called once per frame
    void Update()
    {
        FallClamping();
        InCoyoteTime();
    }
}
