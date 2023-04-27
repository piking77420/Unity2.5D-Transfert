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

    
    
    private AudioSource m_AudioSource;

    [SerializeField]
    private PlayerJump m_PlayerJump;

    [SerializeField, Range(0, 5),Tooltip("Not under 1 seconde due to the animation ")]
    private float CoolDownCanSTranslate = 3;

    private bool rbStatus;
    [SerializeField]
    public bool m_CanTranslate = true;

    private RigidbodyConstraints m_rbContraintBase;



    [SerializeField]
    private Renderer[] m_RenderersGhost;

    [SerializeField]
    private AudioClip m_AudioClip;


    private IEnumerator CoolDownTranslate() 
    {
        yield return new WaitForSeconds(CoolDownCanSTranslate);
        m_CanTranslate = true;
    }

    private void PlayAudio() 
    {
        m_AudioSource.clip = m_AudioClip;
        m_AudioSource.Play();
    }

    public override void OnChangingDimension(InputAction.CallbackContext _context)
    {


        if (_context.canceled && m_CanTranslate && m_PlayerGhost.IsCanPlayerTranslate())
        {
            m_Animator.SetTrigger("Translate");
            PlayAudio();
            DimensionScript.Dimension current = CurrentObjectDimension.CurrentDimension;

            m_Animator.SetInteger("Dimension", (int)current);
            m_CanTranslate = false;
            StartCoroutine(CoolDownTranslate());


        }

    }



    private void SwapSound(DimensionScript.Dimension dimension) 
    {
        if(dimension == DimensionScript.Dimension.Normal) 
        {
            AudioManagers.instance.m_Animator.SetTrigger("TransfertIn");
        }
        else
        {
            AudioManagers.instance.m_Animator.SetTrigger("TransfertOut");

        }
    }



    public override void StartTranslation()
    {
        m_Rigidbody.velocity = Vector3.zero;
        SwapSound(CurrentObjectDimension.CurrentDimension);

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

        foreach (Renderer renderer in m_RenderersGhost)
        {
            renderer.enabled = !renderer.enabled;
        }
        foreach (Renderer renderer in m_Renderers)
        {
            renderer.enabled = !renderer.enabled;
        }

        
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

        foreach (Renderer renderer in m_RenderersGhost)
        {
            renderer.enabled = !renderer.enabled;
        }
        foreach (Renderer renderer in m_Renderers) 
        {
            renderer.enabled = !renderer.enabled;
        }

        m_PlayerJump.isGravityApplie = !m_PlayerJump.isGravityApplie;




        CurrentObjectDimension.SwapDimension();
        m_PlayerGhost.GetComponent<DimensionScript>().SwapDimension();
            
    }





    protected override void Awake()
    {   
        base.Awake();
        m_rbContraintBase = m_Rigidbody.constraints;
        m_PlayerJump = GetComponent<PlayerJump>();
        m_Renderers = transform.GetChild(0).GetComponentsInChildren<SkinnedMeshRenderer>();
        m_RenderersGhost = transform.GetChild(1).GetComponentsInChildren<SkinnedMeshRenderer>();
        m_AudioSource = GetComponent<AudioSource>();

    }
}
