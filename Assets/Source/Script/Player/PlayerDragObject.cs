using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerDragObject : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private PlayerSelectObject m_DragObject;

    [SerializeField]
    private PlayerJump m_PlayerJump;

    [SerializeField]
    private Rigidbody m_rb;

    [SerializeField]
    private Transform m_TransformPlayerDom;

    [Space,SerializeField ,Range(0, 15)]
    private float m_MaxRadius;

    [SerializeField]
    private float m_SelectionRadius;

    [Tooltip("Value of radius increase each second")]
    [SerializeField, Range(1, 5)]
    private float m_SelectRadiusMultiplicator;

    [SerializeField]
    List<SelectableObject> DragAbleObject;

    [SerializeField]
    private CheckIsGround m_IsGround;

    [SerializeField]    
    private bool m_TranslateButton;

    [SerializeField]
    private bool m_ShowRadius;

    [SerializeField]
    private Transform m_PlayerTransform;

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
            m_PlayerTransform = gameObject.transform.GetChild(0).GetComponent<Transform>();
            Gizmos.DrawWireSphere(m_PlayerTransform.position, m_SelectionRadius);
        }
    }
        
    

    private void FindAllDragableObject()
    {
        Collider[] sphereDrag = Physics.OverlapSphere(m_PlayerTransform.position, m_SelectionRadius);

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
            item.gameObject.GetComponent<TranSlate>().isTranslate = true;
            item.gameObject.GetComponent<TranSlate>().OnChangingDimension(_context);
        }
    }

    private void GetPlayerRadius(InputAction.CallbackContext _context) 
    {
        
        if(m_IsGround.isGrounded)   
        switch (_context.phase)
        {
            case InputActionPhase.Started:
                m_TranslateButton = true;
                break;
            case InputActionPhase.Performed:
                m_PlayerJump.isGravityApplie = false;
                break;
            case InputActionPhase.Canceled:
                m_TranslateButton = false;
                FindAllDragableObject();
                TransSlateObject(_context);
                m_SelectionRadius= 0;
                m_TransformPlayerDom.transform.localScale = Vector3.zero;
                m_PlayerJump.isGravityApplie = true;

                DragAbleObject.Clear();
              
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
        m_PlayerTransform = gameObject.transform.GetChild(0).GetComponent<Transform>();
        m_IsGround = GetComponent<CheckIsGround>();
        m_PlayerJump = GetComponent<PlayerJump>();
        m_rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_TranslateButton && m_SelectionRadius <= m_MaxRadius) 
        {   
            float addedValue = Time.deltaTime * m_SelectRadiusMultiplicator;
       

            Vector3 scaleDome = m_TransformPlayerDom.transform.localScale;
            scaleDome.x += addedValue;
            scaleDome.y += addedValue;
            scaleDome.z += addedValue;
            m_TransformPlayerDom.transform.localScale = scaleDome;
            m_SelectionRadius += addedValue;
        }
    }
}
