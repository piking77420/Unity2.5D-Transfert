using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DimensionScript;
using UnityEngine.InputSystem;
using Unity.Jobs;
using UnityEngine.Events;
using Unity.VisualScripting;
using static UnityEngine.Rendering.DebugUI;
using System;


[RequireComponent(typeof(DimensionScript))]
public class TranSlate : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField]
    protected DimensionScript CurrentObjectDimension;



    [SerializeField]
    protected Animator m_Animator;


    [SerializeField]
    protected Renderer m_Renderer;

    [SerializeField]
    protected Rigidbody m_Rigidbody;

    public bool isTranslate;



    private bool m_GravityStatue;




    public virtual void OnChangingDimension(InputAction.CallbackContext _context)
    {
                


            if (_context.canceled && isTranslate)
            {
                m_Animator.SetTrigger("Translate");

                DimensionScript.Dimension current = CurrentObjectDimension.CurrentDimension;

                m_Animator.SetInteger("Dimension", (int)current);
                isTranslate = false;
            }
                
    }




    protected void Start()
    {
        m_Animator.SetInteger("Dimension",(int)CurrentObjectDimension.CurrentDimension);
    }



    protected void Awake()
    {
        CurrentObjectDimension= gameObject.GetComponent<DimensionScript>();
      


        m_Rigidbody = GetComponent<Rigidbody>();
        if(m_Rigidbody == null) 
        {
            m_Rigidbody  = GetComponentInParent<Rigidbody>();
        }

        m_Animator = gameObject.GetComponentInParent<Animator>();
        m_Renderer = gameObject.GetComponent<Renderer>();
       
        if(m_Renderer == null) 
        {
            m_Renderer = GetComponentInChildren<Renderer>();
        }


    }

 

  

    public virtual void  StartTranslation()
    { 

        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.useGravity = !m_Rigidbody.useGravity;
        m_Renderer.enabled = !m_Renderer.enabled;

    }

    public virtual void EndTranslation()
    {

        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.useGravity = !m_Rigidbody.useGravity;
        m_Renderer.enabled = !m_Renderer.enabled;

        CurrentObjectDimension.SwapDimension();

    }



}
