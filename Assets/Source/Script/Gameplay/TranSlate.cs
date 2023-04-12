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
    protected MeshRenderer m_Renderer;

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





    protected void Awake()
    {
        CurrentObjectDimension= gameObject.GetComponent<DimensionScript>();
        m_Rigidbody = GetComponent<Rigidbody>();

        m_Animator = gameObject.GetComponentInParent<Animator>();
        m_Renderer = gameObject.GetComponent<MeshRenderer>();

    }

 

    //void private
  

    public virtual void  Translation() 
    {
        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.useGravity = !m_Rigidbody.useGravity;
        m_Renderer.enabled = !m_Renderer.enabled;

    }



    // Update is called once per frame
 
    
}
