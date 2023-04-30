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

    [Header("Dependencies")]

    [SerializeField]
    protected DimensionScript CurrentObjectDimension;

    [SerializeField]
    protected Animator m_Animator;


    [SerializeField]
    protected Renderer[] m_Renderers;

    [SerializeField,Space(1)]
    protected Rigidbody m_Rigidbody;
    






    public bool isTranslate;

    private bool m_GravityStatue;

    public virtual void OnChangingDimension()
    {

        Debug.Log(isTranslate);

            if ( isTranslate)
            {
                m_Animator.SetTrigger("Translate");

                DimensionScript.Dimension current = CurrentObjectDimension.CurrentDimension;

                m_Animator.SetInteger("Dimension", (int)current);
                isTranslate = false;
            }
                
    }




    protected virtual void Start()
    {
        m_Animator.SetInteger("Dimension",(int)CurrentObjectDimension.CurrentDimension);
    }



    protected virtual void Awake()
    {
        CurrentObjectDimension= gameObject.GetComponent<DimensionScript>();
      


        m_Rigidbody = GetComponent<Rigidbody>();
        if(m_Rigidbody == null) 
        {
            m_Rigidbody  = GetComponentInParent<Rigidbody>();
        }

        m_Animator = gameObject.GetComponentInParent<Animator>();
        m_Renderers = gameObject.GetComponents<Renderer>();
       
        if(m_Renderers == null) 
        {
            m_Renderers = GetComponentsInChildren<Renderer>();
        }


    }

 

  

    public virtual void  StartTranslation()
    { 

        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.useGravity = !m_Rigidbody.useGravity;
        foreach (Renderer renderer in m_Renderers)
        {
            renderer.enabled = !renderer.enabled;
        }

    }

    public virtual void EndTranslation()
    {

        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.useGravity = !m_Rigidbody.useGravity;

        foreach(Renderer renderer in m_Renderers) 
        {
            renderer.enabled = !renderer.enabled;
        }

        CurrentObjectDimension.SwapDimension();

    }



}
