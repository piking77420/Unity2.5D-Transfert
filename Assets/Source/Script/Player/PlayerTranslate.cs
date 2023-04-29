using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerLearningSkill;




public class PlayerTranslate : TranSlate
{
    // Start is called before the first frame update

    [Space]


    [SerializeField]
    private PlayerGhostDetection m_PlayerGhost;





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

    [Header("AudioSource")]

    [SerializeField]
    private AudioClip m_AudioClip;
    [SerializeField]
    private AudioSource[] m_AudioSource;



    [Space,Tooltip("If player has learn this skill")]
    public bool ISLearned;


    private IEnumerator CoolDownTranslate() 
    {
        yield return new WaitForSeconds(CoolDownCanSTranslate);
        m_CanTranslate = true;
    }

    private void PlayAudio() 
    {
        if (this.enabled == true) 
        {
            m_AudioSource[(int)PlayerSkill.Translate].clip = m_AudioClip;
            m_AudioSource[(int)PlayerSkill.Translate].Play();
        }
        
    }


    public void Translate() 
    {
        Debug.Log("sdq");
        if ( m_CanTranslate && m_PlayerGhost.IsCanPlayerTranslate() && ISLearned) 
        {
            m_Animator.SetTrigger("Translate");
            PlayAudio();
            DimensionScript.Dimension current = CurrentObjectDimension.CurrentDimension;

            m_Animator.SetInteger("Dimension", (int)current);
            m_CanTranslate = false;
            StartCoroutine(CoolDownTranslate());
        }
      
    } 

    public void OnChangingDimension(InputAction.CallbackContext _context)
    {


        if (_context.canceled )
        {

            Translate();

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
        AudioManagers.instance.LifeBarCheck(CurrentObjectDimension.CurrentDimension);

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
        AudioManagers.instance.LifeBarCheck(CurrentObjectDimension.CurrentDimension);
        m_PlayerGhost.GetComponent<DimensionScript>().SwapDimension();
            
    }





    protected override void Awake()
    {   
        base.Awake();
        m_rbContraintBase = m_Rigidbody.constraints;
        m_PlayerJump = GetComponent<PlayerJump>();
        m_Renderers = transform.GetChild(0).GetComponentsInChildren<SkinnedMeshRenderer>();
        m_RenderersGhost = transform.GetChild(1).GetComponentsInChildren<SkinnedMeshRenderer>();
        m_AudioSource = GetComponents<AudioSource>();


    }
}
