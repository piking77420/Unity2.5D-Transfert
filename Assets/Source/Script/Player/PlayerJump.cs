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

    [SerializeField]
    private bool m_IsGrounded = false;


    [SerializeField]
    private bool m_IsGravityApplie = true;

    [SerializeField,Range(1,10)]
    private float m_GravityValue = 1;

    [Header("JumpValues")]

    [SerializeField]
    private float m_JumpStrengt;

    [SerializeField]
    private bool m_JumpBuffer;

    [Tooltip("Falling acceleration after he start falling (cant' be superior to")]
    [SerializeField, Range(0, 10)]
    private float fallAcceleration;


    [Tooltip("MaximumValue for falling Velocity")]
    [SerializeField, Range(0, 180)]
    private float FallClampValue;


    [SerializeField, Range(0, 180)]
    private float DesiredAngle;

    [SerializeField, Range(0, 10)]
    private float CoyoteTime;




    [Header("Apex Value")]

    [SerializeField, Range(1, 10)]
    private float m_ApexModifiers;

    [SerializeField, Range(0, 5)]
    private float m_ApexTimerNoGravity;


    private float m_MaxCoyoteTime;


    [Header("NbrOfCollision")]
    private List<Collider> m_Colliders;
    [SerializeField]
    private int m_nbrOfColldier;


    [HideInInspector]
    public Vector3 customGravity;

    IEnumerator ApexModifers() 
    {

        m_IsGravityApplie = false;
        m_PlayerMovement.movement = new Vector3(m_PlayerMovement.movement.x * m_ApexModifiers, m_PlayerMovement.movement.y, m_PlayerMovement.movement.z); 
        yield return new WaitForSeconds(m_ApexTimerNoGravity);
        m_IsGravityApplie = true;
    }


    private void DoJump() 
    {
        StartCoroutine(ApexModifers());
        m_Rigidbody.velocity = Vector3.up * m_JumpStrengt;
        m_IsGrounded = false;
        m_JumpBuffer = false;
    }




  
    public void OnJumping(InputAction.CallbackContext _callbackContext)
    {
        bool Onpress = _callbackContext.performed;

        JumpBuffer();




        if (Onpress) 
        {

            if (!m_IsGrounded && !m_JumpBuffer)
            {
                m_JumpBuffer = true;
                return;
            }

            if (m_IsGrounded && !m_JumpBuffer)
            {
                DoJump();

            }

          

        }
        else
        {
            return;
        }


      
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

        return false;

    }

    private void OnCollisionExit(Collision collision)
    {
       m_Colliders.Remove(collision.collider);
    }



    private void OnCollisionEnter(Collision collision)
    {
        m_Colliders.Add(collision.collider);
        CoyoteTime = m_MaxCoyoteTime;


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
        m_Colliders = new List<Collider>();
        
    }

    void Start()
    {
        
    }



    void FallClamping() 
    {
        
        if (m_Rigidbody.velocity.y < 0 && m_IsGravityApplie)
        {
            if (m_Rigidbody.velocity.y < -FallClampValue && !m_IsGrounded)
            {
                m_Rigidbody.velocity = new Vector3(m_PlayerMovement.movement.x, -FallClampValue, m_PlayerMovement.movement.y);
            }
            else
            {
                m_Rigidbody.velocity += Vector3.up * customGravity.y * (fallAcceleration - 1) * Time.deltaTime;
            }
        }
    }


    void InCoyoteTime() 
    {
      

        if(m_Colliders.Count == 0 && m_IsGrounded) 
        {
            CoyoteTime -= Time.deltaTime;

            if (CoyoteTime < 0) 
            {
                m_IsGrounded = false;
                CoyoteTime = m_MaxCoyoteTime;
            }


        }
    }



    private void AddGravity() 
    {
        if (m_IsGravityApplie) 
        {
            Vector3 gravity =  (m_Rigidbody.velocity);
            gravity.y = Physics.gravity.y * m_GravityValue; 
            m_Rigidbody.AddForce(customGravity);
        }
    }

    private void CalculateCustomGavrity() 
    {
        customGravity = m_Rigidbody.velocity;
        customGravity.y = m_GravityValue * Physics.gravity.y * m_Rigidbody.mass ;
    }
    private void JumpBuffer()
    {
        if (m_IsGrounded && m_JumpBuffer)
        {
            DoJump();
        }

        if (m_IsGrounded)
        {
            m_JumpBuffer = false;
        }


      

    }


    // Update is called once per frame
    void FixedUpdate()
    {

        m_Rigidbody.useGravity = !m_IsGravityApplie;
        m_nbrOfColldier = m_Colliders.Count;
        JumpBuffer();
        CalculateCustomGavrity();
        AddGravity();
        FallClamping();
        InCoyoteTime();
    }
}
