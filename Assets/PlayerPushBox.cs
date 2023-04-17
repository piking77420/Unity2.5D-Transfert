using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPushBox : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField, Range(1, 1000)]
    private float PushStrenght = 10;

    private float m_PushVector;

    [SerializeField]
    private GameObject m_CurrentBox;


    [SerializeField]
    private Transform m_PlayerPos;

    private Rigidbody m_PlayerRigidBody;
    private Rigidbody m_BoxRb;

    [SerializeField, Range(1, 20)]
    private float MaxPushVeclocity;

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
    [SerializeField]
    private bool m_ShowRange;


    [SerializeField, Range(0, 10)]
    private float maxBoxVelocity;

    [SerializeField, Range(0, 10)]
    private float m_RangeBetweenPlayerAndBox;



    [SerializeField]
    private ConfigurableJoint m_ConfigurableJoint;

    private float baseSpeed;


    private void PlayerQuitInteraction()
    {
        DropBox();
        m_CurrentBox = null;
        m_BoxRb = null;
        m_PhysicsMaterialBox = null;
        m_PlayerPhysicsMaterial.staticFriction = m_baseStaticFriction;
        m_PlayerPhysicsMaterial.dynamicFriction = m_baseDynamicFriction;
        m_PlayerInput.SwitchCurrentActionMap("Gameplay");
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

    public void GetBox(InteractableObject @object)
    {

        m_CurrentBox = @object.gameObject;
        m_BoxRb = m_CurrentBox.GetComponentInParent<Rigidbody>();
        m_PhysicsMaterialBox = @object.GetComponent<Collider>().material;
        m_PlayerInput.SwitchCurrentActionMap("MoveBox");
   
        m_PlayerPhysicsMaterial.staticFriction = m_PhysicsMaterialBox.staticFriction;
        m_PlayerPhysicsMaterial.dynamicFriction = m_PhysicsMaterialBox.dynamicFriction;

        m_ConfigurableJoint = @object.transform.parent.GetComponent<ConfigurableJoint>();
        ChangeSpeed();

    }




    private void Awake()
    {
        m_PlayerPos = transform.GetChild(0).GetComponent<Transform>();
        m_PlayerInput = GetComponent<PlayerInput>();
        m_PlayerRigidBody = GetComponent<Rigidbody>();
        m_PlayerMovment = GetComponent<PlayerMovement>();
        m_PlayerPhysicsMaterial = GetComponentInChildren<Collider>().material;
        m_baseStaticFriction = m_PlayerPhysicsMaterial.staticFriction;
        m_baseDynamicFriction = m_PlayerPhysicsMaterial.dynamicFriction;
        m_PlayerJump = GetComponent<PlayerJump>();




    }

    void Start()
    {

    }


    private void DropBox() 
    {
        m_ConfigurableJoint.connectedBody = null;
        m_ConfigurableJoint.xMotion = ConfigurableJointMotion.Locked;
        m_ConfigurableJoint.yMotion = ConfigurableJointMotion.Free;
        m_ConfigurableJoint.zMotion = ConfigurableJointMotion.Locked;
        m_ConfigurableJoint.angularXMotion = ConfigurableJointMotion.Free;
        m_ConfigurableJoint.angularYMotion = ConfigurableJointMotion.Free;
        m_ConfigurableJoint.angularZMotion = ConfigurableJointMotion.Limited;
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
            dir.Set(dir.x, m_CurrentBox.transform.position.y, dir.z);

            Ray r = new Ray(playerPos, dir);

            if (Physics.Raycast(r, out RaycastHit hit, m_RangeBetweenPlayerAndBox) && hit.rigidbody == m_BoxRb)
            {

                TakeBox();

            }
            else
            {
                DropBox();
                 m_CurrentBox = null;
            }















        }
        else
        {
         
            PlayerQuitInteraction();

        }



    }


 

}



