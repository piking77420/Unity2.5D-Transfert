using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDragObject : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private PlayerSelectObject m_DragObject;


    [SerializeField]
    private bool m_ShowRadius;


    [SerializeField, Range(1, 15)]
    private float m_SelectionRadius;

    [SerializeField]
    List<SelectableObject> DragAbleObject;

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
            if(collider.gameObject.TryGetComponent<SelectableObject>(out SelectableObject selectable)) 
            {
                DragAbleObject.Add(selectable);
            }
        }


    }



    public void OnChangingDimension(InputAction.CallbackContext _context)
    {
        FindAllDragableObject();


        if (_context.performed)
        {
            foreach (var item in DragAbleObject)
            {
                item.gameObject.GetComponent<TranSlate>().OnChangingDimension(_context);
            }
        }

        DragAbleObject.Clear();
    }





    private void Awake()
    {
        DragAbleObject = new List<SelectableObject>();
        m_DragObject = GetComponent<PlayerSelectObject>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
