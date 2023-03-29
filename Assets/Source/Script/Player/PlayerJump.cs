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

    [Header("JumpValues")]

    [SerializeField]
    private float m_JumpStrengt;


    [Tooltip("Falling acceleration after he start falling (cant' be superior to")]
    [SerializeField, Range(0, 180)]
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

    IEnumerator ApexModifers() 
    {
        
        m_Rigidbody.useGravity = false;
        m_PlayerMovement.movement = new Vector3(m_PlayerMovement.movement.x * m_ApexModifiers, m_PlayerMovement.movement.y, m_PlayerMovement.movement.z); 
        yield return new WaitForSeconds(m_ApexTimerNoGravity);
        m_Rigidbody.useGravity = true;
    }



    public void OnJumping(InputAction.CallbackContext _callbackContext)
    {
        if (m_IsGrounded)
        {
            StartCoroutine(ApexModifers());
            m_Rigidbody.velocity = Vector3.up * m_JumpStrengt;
        }
        m_IsGrounded = false;

    }

  

    bool CheckCollsion(Collision collision)
    {
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            float getangle = Vector3.Angle(collision.contacts[i].normal, Vector3.up);

            Debug.Log(getangle);
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

    



    // Update is called once per frame
    void FixedUpdate()
    {
        m_nbrOfColldier = m_Colliders.Count;
        FallClamping();
        InCoyoteTime();
    }
}
