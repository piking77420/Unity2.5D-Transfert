using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using static PlayerLearningSkill;


public class PlayerDragObject : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Dependencies")]

    [SerializeField]
    private PlayerSelectObject m_DragObject;

    [SerializeField]
    private PlayerJump m_PlayerJump;

    [SerializeField]
    private CheckIsGround m_IsGround;

    [SerializeField]
    private Transform m_PlayerTransform;

    [SerializeField]
    private PlayerTranslate m_PlayerTranslate;

    [SerializeField]
    private Rigidbody m_rb;

    [SerializeField]
    private Transform m_TransformPlayerDom;

    [SerializeField]
    private Renderer m_DomeRender;

    [Space]

    [Header("Value")]
    [Space,SerializeField ,Range(0, 15)]
    private float m_MaxRadius;

    [SerializeField]
    private float m_SelectionRadius;

    [Tooltip("Value of radius increase each second")]
    [SerializeField, Range(1, 5)]
    private float m_SelectRadiusMultiplicator;

    [SerializeField, Range(0, 5)]
    private float m_MinimRadiusValue;

    [SerializeField]
    List<SelectableObject> DragAbleObject;


    [SerializeField]    
    private bool m_TranslateButton;

    [SerializeField]
    private bool m_ShowRadius;


    [SerializeField]
    public bool IsLearned;

    [Header("Audio")]
    [SerializeField]
    private AudioSource[] m_AudioSource;

    [SerializeField]
    private AudioClip m_Clip;

    private bool IsObjectSameDimenSionHasPlayer(SelectableObject @object) 
    {
        @object.gameObject.TryGetComponent<DimensionScript>(out DimensionScript script);
        TryGetComponent<DimensionScript>(out DimensionScript playerDimension);

        if (script.CurrentDimension == playerDimension.CurrentDimension) 
        {
            return true;
        }

        return false;
    }


    private void OnDrawGizmos()
    {
        if (m_ShowRadius)
        {
            Gizmos.color = Color.green;
            
            Gizmos.DrawWireSphere(m_TransformPlayerDom.transform.position, m_SelectionRadius);
        }
    }
        
    

    private void FindAllDragableObject()
    {
        Collider[] sphereDrag = Physics.OverlapSphere(m_DomeRender.transform.position, m_SelectionRadius);

        foreach (Collider collider in sphereDrag)
        {
            
            if(collider.gameObject.TryGetComponent<SelectableObject>(out SelectableObject selectable) && IsObjectSameDimenSionHasPlayer(selectable)) 
            {
                DragAbleObject.Add(selectable);
            }
        }


    }

    private void TransSlateObject(InputAction.CallbackContext _context) 
    {

        foreach (var item in DragAbleObject)
        {
            if(m_SelectionRadius >= m_MinimRadiusValue) 
            {
                item.gameObject.GetComponent<TranSlate>().isTranslate = true;
                item.gameObject.GetComponent<TranSlate>().OnChangingDimension(_context);
            }
        
        }
    }

    private void GetPlayerRadius(InputAction.CallbackContext _context) 
    {
        if (!IsLearned)
            return;




        bool canDragObject = m_PlayerTranslate.m_CanTranslate && m_IsGround.isGrounded;


            switch (_context.phase)
            {
                case InputActionPhase.Started:
                    if (canDragObject) 
                    {

                        m_TranslateButton = true;
                        m_AudioSource[(int)PlayerSkill.DragObject].Play();
                    }
                    break;
                case InputActionPhase.Performed:
                    break;
                case InputActionPhase.Canceled:
                    m_TranslateButton = false;
                    m_AudioSource[(int)PlayerSkill.DragObject].Stop();
                    FindAllDragableObject();
                    TransSlateObject(_context);
                    m_SelectionRadius = 0;
                    m_TransformPlayerDom.transform.localScale = Vector3.zero;
                    m_PlayerJump.isGravityApplie = true;

                    DragAbleObject.Clear();
                    m_DomeRender.enabled = false;

                    break;
            }
        
   


    }



    public void OnChangingDimension(InputAction.CallbackContext _context)
    {

        GetPlayerRadius(_context);

    }





    private void Awake()
    {
        DragAbleObject = new List<SelectableObject>();
        m_DragObject = GetComponent<PlayerSelectObject>();
        m_TransformPlayerDom.transform.localScale = Vector3.zero;
        m_IsGround = GetComponent<CheckIsGround>();
        m_PlayerJump = GetComponent<PlayerJump>();
        m_rb = GetComponent<Rigidbody>();
        m_PlayerTranslate = GetComponent<PlayerTranslate>();
        m_DomeRender = m_TransformPlayerDom.gameObject.GetComponent<Renderer>();
        m_AudioSource = GetComponents<AudioSource>();
        m_AudioSource[(int)PlayerSkill.DragObject].clip = m_Clip;


    }
    void Start()
    {
        m_DomeRender.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_TranslateButton && m_SelectionRadius  <= m_MaxRadius && m_PlayerTranslate.m_CanTranslate) 
        {
            float addedValue = Time.deltaTime * m_SelectRadiusMultiplicator;

            if (m_SelectionRadius >= m_MinimRadiusValue) 
            {
                m_DomeRender.enabled = true;
            }

            m_SelectionRadius += addedValue;
            Vector3 scaleDome = new Vector3(addedValue, addedValue, addedValue);
            m_TransformPlayerDom.transform.localScale += scaleDome;


        }
    }
}
