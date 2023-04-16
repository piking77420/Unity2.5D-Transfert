using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPushBox : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField,Range(1,50)]
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
    private PhysicMaterial m_PlayerPhysicsMaterial;

    private float m_baseStaticFriction;
    private float m_baseDynamicFriction;


    [SerializeField]
    private PhysicMaterial m_PhysicsMaterialBox;



    private bool m_IsInRange;
    [SerializeField]
    private bool m_ShowRange;





    public void OnQuitBoxInteraction(InputAction.CallbackContext _callbackContext) 
    {
        if (_callbackContext.performed) 
        {
            m_CurrentBox = null;
            m_BoxRb = null;
            m_PhysicsMaterialBox = null;
            m_PlayerPhysicsMaterial.staticFriction = m_baseStaticFriction;
            m_PlayerPhysicsMaterial.dynamicFriction = m_baseDynamicFriction;
            m_PlayerInput.SwitchCurrentActionMap("Gameplay");
            m_PlayerMovment.enabled = true;
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

    public void GetBox(InteractableObject @object) 
    {

        m_CurrentBox = @object.gameObject;
        m_BoxRb = m_CurrentBox.GetComponentInParent<Rigidbody>();
        m_PhysicsMaterialBox = @object.GetComponent<Collider>().material;
        m_PlayerInput.SwitchCurrentActionMap("MoveBox");
        m_PlayerMovment.enabled = false;
        m_PlayerPhysicsMaterial.staticFriction = m_PhysicsMaterialBox.staticFriction;
        m_PlayerPhysicsMaterial.dynamicFriction = m_PhysicsMaterialBox.dynamicFriction;


    }


   

    private void Awake()
    {
        m_PlayerPos = transform.GetChild(0).GetComponent<Transform>();
        m_PlayerInput = GetComponent<PlayerInput>();
        m_PlayerRigidBody= GetComponent<Rigidbody>();
        m_PlayerMovment = GetComponent<PlayerMovement>();
        m_PlayerPhysicsMaterial = GetComponentInChildren<Collider>().material;
        m_baseStaticFriction = m_PlayerPhysicsMaterial.staticFriction;
        m_baseDynamicFriction = m_PlayerPhysicsMaterial.dynamicFriction;


    }

    void Start()
    {
        
    }


    

    // Update is called once per frame
    void FixedUpdate()
    {
       
        if (m_CurrentBox != null) 
        {

            Vector3 boxPos = m_CurrentBox.GetComponentInParent<Transform>().position;
            Vector3 playerPos = m_PlayerPos.transform.position;

            Vector3 dir = (boxPos - playerPos);
            dir.Set(dir.x, m_CurrentBox.transform.position.y, dir.z);

            Ray r = new Ray(playerPos, dir);
            Debug.DrawRay(playerPos, dir, Color.red, 0);

            if (Physics.Raycast(r, out RaycastHit hit) && hit.rigidbody == m_BoxRb)  
            {
                
                m_PlayerMovment.enabled = false;
                Vector3 pushVector = new Vector3(m_PushVector, 0, 0) * PushStrenght ;
                

                m_BoxRb.AddForceAtPosition(pushVector * m_BoxRb.mass, hit.point);
                m_PlayerRigidBody.AddForce(pushVector *  m_PlayerRigidBody.mass);

                
            }
            else 
            {
                m_PlayerMovment.enabled = true;
                Debug.Log("out");
                m_CurrentBox = null;
            }



        }
    }

}
