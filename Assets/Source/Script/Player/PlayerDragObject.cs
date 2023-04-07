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
    private bool m_TranslateButton;

    [SerializeField]
    private bool m_ShowRadius;



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
            Gizmos.DrawWireSphere(gameObject.transform.position, m_SelectionRadius);
        }
    }

    

    private void FindAllDragableObject()
    {
        Collider[] sphereDrag = Physics.OverlapSphere(transform.position, m_SelectionRadius);

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
            item.gameObject.GetComponent<TranSlate>().OnChangingDimension(_context);
        }
    }

    private void GetPlayerRadius(InputAction.CallbackContext _context) 
    {
        switch (_context.phase)
        {
            case InputActionPhase.Started:
                m_TranslateButton = true;
                break;
            case InputActionPhase.Performed:
                break;
            case InputActionPhase.Canceled:
                m_TranslateButton = false;
                FindAllDragableObject();
                TransSlateObject(_context);
                m_SelectionRadius= 0;
                m_TransformPlayerDom.transform.localScale = Vector3.zero;
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
