using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static AudioManagers.BiomeStat;
using static AudioManagers.SourceFrom;


public class PlayerPushBox : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Value")]





    [SerializeField, Range(0, 100)]
    private float PlayerVelocityMultiplicator;

    private float PlayerVelocityOnPushBox;

    [SerializeField, Range(0, 10), Space(2)]
    private float m_RangeBetweenPlayerAndBox;

    private float m_PushVector;

    [Header("Dependancies")]


    [SerializeField]
    private Transform m_PlayerPos;

    [SerializeField]
    private PlayerPopMenue m_PlayerPopMenue;
    [SerializeField]
    private PlayerMovement m_PlayerMovement;

    private Rigidbody m_PlayerRigidBody;
    private Rigidbody m_BoxRb;

    public GameObject m_CurrentBox { get; private set; }



    [SerializeField]
    private ConfigurableJoint m_ConfigurableJoint;

    [SerializeField]
    private PlayerInput m_PlayerInput;

    [SerializeField]
    private PlayerMovement m_PlayerMovment;

    [SerializeField]
    private PlayerJump m_PlayerJump;


    [SerializeField]
    private PhysicMaterial m_PlayerPhysicsMaterial;

    private float m_baseStaticFriction;
    private float m_baseDynamicFriction;


    [SerializeField]
    private PhysicMaterial m_PhysicsMaterialBox;



    private bool m_IsInRange;


    [SerializeField, Space(2)]
    private bool m_ShowRange;




    [SerializeField, Tooltip("Show Minimal range for player Grab box")]
    private bool ShowGizmo;


    private float baseSpeed;

    [SerializeField]
    private LayerMask m_LayerMask;



    [SerializeField]
    private float m_BoxWantedMass;



    private float m_BaseMass;


    [Header("AudioPart"),SerializeField]
    private AudioSource m_AudioSource;


    private void OnDrawGizmos()
    {
        if (ShowGizmo)
        {
            Gizmos.color = Color.yellow;
            //  Gizmos.DrawCube(m_PlayerPos.position, new Vector3(m_RangeBetweenPlayerAndBox, m_RangeBetweenPlayerAndBox, m_RangeBetweenPlayerAndBox));

            Gizmos.DrawWireSphere(m_PlayerPos.position, 2);
        }
    }


    private void PlayerQuitInteraction()
    {
        DropBox();
        m_CurrentBox = null;
        m_BoxRb = null;
        m_PhysicsMaterialBox = null;
        m_PlayerPhysicsMaterial.staticFriction = m_baseStaticFriction;
        m_PlayerPhysicsMaterial.dynamicFriction = m_baseDynamicFriction;
    }



    public void OnQuitBoxInteraction(InputAction.CallbackContext _callbackContext)
    {
        if (_callbackContext.performed)
        {
            PlayerQuitInteraction();
            m_PlayerMovment._Speed = baseSpeed;
        }

    }


    public void OnPushBox(InputAction.CallbackContext _callbackContext)
    {
        if (_callbackContext.performed)
        {
            if (!m_AudioSource.isPlaying) 
            {
                AudioManagers.instance.PlayAudioAt(AudioManagers.SourceFrom.FemaleEffort, AudioManagers.BiomeStat.Interior, m_AudioSource);
                m_AudioSource.Play();
            }

            m_PushVector = _callbackContext.ReadValue<float>();
        }

        if (_callbackContext.canceled)
        {
            m_PushVector = 0;
        }
    }


    private void ChangeSpeed()
    {
        baseSpeed = m_PlayerMovment._Speed;
        m_PlayerMovment._Speed /= 2;
    }

    public void GetBox(InteractableObject @object, Vector3 Playerpos)
    {


        if (Vector3.Distance(Playerpos, @object.transform.position) < m_RangeBetweenPlayerAndBox)
        {
            m_CurrentBox = @object.gameObject;
            m_BoxRb = m_CurrentBox.GetComponentInParent<Rigidbody>();
            m_PhysicsMaterialBox = @object.GetComponent<Collider>().material;
            m_PlayerInput.SwitchCurrentActionMap("MoveBox");

            m_PlayerPhysicsMaterial.staticFriction = m_PhysicsMaterialBox.staticFriction;
            m_PlayerPhysicsMaterial.dynamicFriction = m_PhysicsMaterialBox.dynamicFriction;

            m_ConfigurableJoint = @object.transform.parent.GetComponent<ConfigurableJoint>();
            ChangeSpeed();

            m_BaseMass = m_BoxRb.mass;
            m_BoxRb.mass = m_BoxWantedMass;

        }


    }




    private void Awake()
    {
        m_PlayerInput = GetComponent<PlayerInput>();
        m_PlayerRigidBody = GetComponent<Rigidbody>();
        m_PlayerMovment = GetComponent<PlayerMovement>();
        m_PlayerPhysicsMaterial = GetComponentInChildren<Collider>().material;
        m_baseStaticFriction = m_PlayerPhysicsMaterial.staticFriction;
        m_baseDynamicFriction = m_PlayerPhysicsMaterial.dynamicFriction;
        m_PlayerJump = GetComponent<PlayerJump>();
        m_PlayerMovement = GetComponent<PlayerMovement>();

        m_AudioSource  = GetComponent<AudioSource>();   
        m_PlayerPopMenue = GetComponent<PlayerPopMenue>();  
    }

    void Start()
    {
        PlayerVelocityOnPushBox = m_PlayerMovment._Speed;
    }


    private void DropBox()
    {
        m_BoxRb.mass = m_BaseMass;
        m_ConfigurableJoint.connectedBody = null;
        m_ConfigurableJoint.xMotion = ConfigurableJointMotion.Locked;
        m_ConfigurableJoint.yMotion = ConfigurableJointMotion.Free;
        m_ConfigurableJoint.zMotion = ConfigurableJointMotion.Locked;
        m_ConfigurableJoint.angularXMotion = ConfigurableJointMotion.Free;
        m_ConfigurableJoint.angularYMotion = ConfigurableJointMotion.Free;
        m_ConfigurableJoint.angularZMotion = ConfigurableJointMotion.Limited;
        m_PlayerInput.SwitchCurrentActionMap("Gameplay");

    }

    private void TakeBox()
    {


        m_ConfigurableJoint.enableCollision = true;
        m_ConfigurableJoint.connectedBody = m_PlayerRigidBody;

        m_ConfigurableJoint.xMotion = ConfigurableJointMotion.Locked;
        m_ConfigurableJoint.yMotion = ConfigurableJointMotion.Free;
        m_ConfigurableJoint.zMotion = ConfigurableJointMotion.Locked;
        m_ConfigurableJoint.angularXMotion = ConfigurableJointMotion.Free;
        m_ConfigurableJoint.angularYMotion = ConfigurableJointMotion.Free;
        m_ConfigurableJoint.angularZMotion = ConfigurableJointMotion.Limited;

    }




    // Update is called once per frame
    void FixedUpdate()
    {


        if (m_CurrentBox != null)
        {




            Vector3 boxPos = m_CurrentBox.GetComponentInParent<Transform>().position;
            Vector3 playerPos = m_PlayerPos.transform.position;

            // get Vector From box dPLayer 

            Vector3 dir = (boxPos - playerPos);
            dir.Set(dir.x, 0, dir.z);

            Ray r = new Ray(playerPos, dir);


            if (Physics.Raycast(r, out RaycastHit hit, m_RangeBetweenPlayerAndBox, m_LayerMask) && hit.rigidbody == m_BoxRb)
            {
                m_PlayerMovement._Speed = PlayerVelocityOnPushBox * PlayerVelocityMultiplicator;
                TakeBox();
            }
            else
            {
                DropBox();
                m_CurrentBox = null;
            }
        }
    }
}









