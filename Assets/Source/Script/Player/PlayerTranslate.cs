using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;





public class PlayerTranslate : TranSlate
{
    // Start is called before the first frame update

    [Space]


    [SerializeField]
    private PlayerGhostDetection m_PlayerGhost;

    [SerializeField]
    private MeshRenderer m_PlayerGraphics;

    [SerializeField]
    private PlayerJump m_PlayerJump;

    [SerializeField, Range(0, 5),Tooltip("Not under 1 seconde due to the animation ")]
    private float CoolDownCanSTranslate = 3;

    private bool rbStatus;
    [SerializeField]
    public bool m_CanTranslate = true;

    private RigidbodyConstraints m_rbContraintBase;


    private IEnumerator CoolDownTranslate() 
    {
        yield return new WaitForSeconds(CoolDownCanSTranslate);
        m_CanTranslate = true;
    }


    public override void OnChangingDimension(InputAction.CallbackContext _context)
    {


        if (_context.canceled && m_CanTranslate && m_PlayerGhost.IsCanPlayerTranslate())
        {
            m_Animator.SetTrigger("Translate");

            DimensionScript.Dimension current = CurrentObjectDimension.CurrentDimension;

            m_Animator.SetInteger("Dimension", (int)current);
            m_CanTranslate = false;
            StartCoroutine(CoolDownTranslate());

        }

    }





    public override void StartTranslation()
    {
        m_Rigidbody.velocity = Vector3.zero;
  

        if(rbStatus == false) 
        {
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            rbStatus = true;
        }
        else 
        {
            m_Rigidbody.constraints = m_rbContraintBase;
            rbStatus = false;
        }



        m_PlayerGhost.GetComponent<MeshRenderer>().enabled = !m_PlayerGhost.GetComponent<Renderer>().enabled;
        m_PlayerGraphics.enabled = !m_PlayerGraphics.enabled;
        m_PlayerJump.isGravityApplie = !m_PlayerJump.isGravityApplie;


    }

    public override void EndTranslation()
    {


        m_Rigidbody.velocity = Vector3.zero;


        if (rbStatus == false)
        {
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            rbStatus = true;
        }
        else
        {
            m_Rigidbody.constraints = m_rbContraintBase;
            rbStatus = false;
        }

        m_PlayerGhost.GetComponent<MeshRenderer>().enabled = !m_PlayerGhost.GetComponent<Renderer>().enabled;
        m_PlayerGraphics.enabled = !m_PlayerGraphics.enabled;
        m_PlayerJump.isGravityApplie = !m_PlayerJump.isGravityApplie;


        CurrentObjectDimension.SwapDimension();
        m_PlayerGhost.GetComponent<DimensionScript>().SwapDimension();
            
    }





    protected override void Awake()
    {   
        base.Awake();
        m_rbContraintBase = m_Rigidbody.constraints;
        m_PlayerJump = GetComponent<PlayerJump>();
        m_PlayerGraphics = GetComponentInChildren<MeshRenderer>();

    }
}
