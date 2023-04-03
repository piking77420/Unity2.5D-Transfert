using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerJump : MonoBehaviour, PlayableAudioScript
{
    // Start is called before the first frame update

    [SerializeField]
    public SelectAudioSource selectAudioSource;



    [Header("Physics Part")]


    [Space,SerializeField]
    private Rigidbody m_Rigidbody;

    [SerializeField]
    private PlayerMovement m_PlayerMovement;

    [SerializeField]
    private bool m_IsGrounded = false;


    [SerializeField]
    public bool isGravityApplie = true;

    [SerializeField,Range(1,10)]
    private float m_GravityValue = 1;

    [Header("JumpValues")]

    [SerializeField]
    private float m_JumpStrengt;
    private bool m_HasPressButton;
    private bool m_WillJump;


    [Tooltip("Max Player Multipaction of the Jump Streght ")]
    [SerializeField,Range(1,4)]
    private float m_MaxJumpMultiplication;


    [SerializeField,Range(1,2)]
    private float m_JumpMultiplactation;




    [SerializeField]
    private bool m_JumpBuffer;

    [Tooltip("Falling acceleration after he start falling (cant' be superior to Climping")]
    [SerializeField, Range(0, 10)]
    private float fallAcceleration;


    [Tooltip("MaximumValue for falling Velocity")]
    [SerializeField, Range(0, 180)]
    private float FallClampValue;


    [Space]

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

        isGravityApplie = false;
        m_PlayerMovement.movement = new Vector3(m_PlayerMovement.movement.x * m_ApexModifiers, m_PlayerMovement.movement.y, m_PlayerMovement.movement.z);
        yield return new WaitForSeconds(m_ApexTimerNoGravity);
        isGravityApplie = true;
    }


    public void DoJump(float JumpStrenght) 
    {

        StartCoroutine(ApexModifers());
        m_Rigidbody.velocity = Vector3.up * JumpStrenght;
        m_IsGrounded = false;
        m_JumpBuffer = false;
        m_WillJump = false;
        m_JumpMultiplactation = 1;
    }




  
    public void OnJumping(InputAction.CallbackContext _callbackContext)
    {
        bool Onpress = _callbackContext.performed;
        float time = (float)_callbackContext.duration;
       
        switch (_callbackContext.phase) 
        {
            case InputActionPhase.Started:
                // Debug.Log(_callbackContext.interaction.ToString() + " Started");
                m_HasPressButton = true;

                break;
            case InputActionPhase.Performed:
               
                break;
            case InputActionPhase.Canceled:
                m_HasPressButton = false;
                m_WillJump = true;
                break;
        }

        

        if (m_WillJump) 
        {
            if (!m_IsGrounded && !m_JumpBuffer)
            {
                m_JumpBuffer = true;
                return;
            }

            if (m_IsGrounded && !m_JumpBuffer)
            {
                DoJump(m_JumpStrengt * m_JumpMultiplactation);

            }

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


    private  void Awake()
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
        
        if (m_Rigidbody.velocity.y < 0 && isGravityApplie)
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
        if (isGravityApplie) 
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
            DoJump(m_JumpStrengt);
        }

        if (m_IsGrounded)
        {
            m_JumpBuffer = false;
        }

    }



    private void GetJumpMultiplication() 
    {
        if (m_HasPressButton)
        {
            m_JumpMultiplactation += (m_JumpMultiplactation) *  Time.deltaTime;
            Mathf.Clamp(m_JumpMultiplactation, 1, m_MaxJumpMultiplication);        }

    }



    // Update is called once per frame
    private void Update()
    {
        GetJumpMultiplication();
        
    }

    public void PlayAudio()
    {
        throw new System.NotImplementedException();
    }





    

    void FixedUpdate()
    {

        m_Rigidbody.useGravity = !isGravityApplie;
        m_nbrOfColldier = m_Colliders.Count;
        JumpBuffer();
        CalculateCustomGavrity();
        AddGravity();
        FallClamping();
        InCoyoteTime();

        
    }

   

}
