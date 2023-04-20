using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenClose : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private LeverAction m_LeverAction;


    [SerializeField]
    private Vector3 m_ReachPos;


    [Header("Gizmo Setting")]

    [SerializeField]
    private bool m_ShowGizmo;

    [SerializeField, Range(0, 5)]
    private float m_SizeOfGizmo;



    private Vector3 m_StartPos;

    private void OnDrawGizmos()
    {
        if (m_ShowGizmo)
        {
            Gizmos.color = Color.blue;


            Gizmos.DrawWireSphere(m_ReachPos + this.transform.position, m_SizeOfGizmo);
        }
    }


    private void Awake()
    {
        
        m_LeverAction = transform.parent.parent.GetComponentInChildren<LeverAction>();
        m_LeverAction.OnAccomplish.AddListener(UpdateDoor);

    }

    private void Start()
    {
        m_StartPos = this.transform.position;
    }


    private void UpdateDoor(float leverAction)
    {
        this.transform.position = Vector3.Lerp(m_StartPos, m_StartPos + m_ReachPos, leverAction);
    }





}
