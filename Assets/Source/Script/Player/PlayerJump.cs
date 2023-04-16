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
    private AudioSource m_AudioSource;



    [Header("Physics Part")]


    [Space,SerializeField]
    private Rigidbody m_Rigidbody;

    [SerializeField]
    private PlayerMovement m_PlayerMovement;

    [SerializeField]
    private CheckIsGround m_CheckIsGround;




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

    /*
    [SerializeField, Range(0, 180)]
    private float DesiredAngle;*/

    [SerializeField, Range(0, 10)]
    private float CoyoteTime;


    [Header("Apex Value")]

    [SerializeField, Range(1, 10)]
    private float m_ApexModifiers;

    [SerializeField, Range(0, 5)]
    private float m_ApexTimerNoGravity;




    private float m_MaxCoyoteTime;





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
        m_AudioSource.Play();
        StartCoroutine(ApexModifers());
        m_Rigidbody.velocity = Vector3.up * JumpStrenght;
        m_CheckIsGround.isGrounded = false;
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
            if (!m_CheckIsGround.isGrounded && !m_JumpBuffer)
            {
                m_JumpBuffer = true;
                return;
            }

            if (m_CheckIsGround.isGrounded && !m_JumpBuffer)
            {
                DoJump(m_JumpStrengt * m_JumpMultiplactation);

            }

        }
     
        


      
    }




    private void OnCollisionEnter(Collision collision)
    {
        CoyoteTime = m_MaxCoyoteTime;
    }


    private  void Awake()
    {
        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_MaxCoyoteTime = CoyoteTime;
        m_CheckIsGround = GetComponent<CheckIsGround>();


    }

    void Start()
    {
        m_Rigidbody.useGravity = false;

    }



    void FallClamping() 
    {
        
        if (m_Rigidbody.velocity.y < 0 && isGravityApplie)
        {
            if (m_Rigidbody.velocity.y < -FallClampValue && !m_CheckIsGround.isGrounded)
            {
                m_Rigidbody.velocity = new Vector3(m_PlayerMovement.movement.x, -FallClampValue, m_PlayerMovement.movement.y);
            }
            else
            {
                Debug.Log("here");
                //m_Rigidbody.velocity += Vector3.up * customGravity.y * (fallAcceleration - 1) * Time.fixedDeltaTime;
            }
        }
    }


    void InCoyoteTime() 
    {
      

        if(m_CheckIsGround.colliders.Count <= 0 && m_CheckIsGround.isGrounded) 
        {
            CoyoteTime -= Time.deltaTime;

            if (CoyoteTime < 0) 
            {
                m_CheckIsGround.isGrounded = false;
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
        if (m_CheckIsGround.isGrounded && m_JumpBuffer)
        {
            DoJump(m_JumpStrengt);
        }

        if (m_CheckIsGround.isGrounded)
        {
            m_JumpBuffer = false;
        }

    }



    private void GetJumpMultiplication() 
    {
        if (m_HasPressButton && m_JumpMultiplactation <= m_MaxJumpMultiplication)
        {
           
            m_JumpMultiplactation += (m_JumpMultiplactation) *  Time.deltaTime;
        }

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
        JumpBuffer();
        CalculateCustomGavrity();
        AddGravity();
        FallClamping();
        InCoyoteTime();

        
    }

   

}
