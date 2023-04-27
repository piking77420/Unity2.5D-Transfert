using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorOpenClose : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private LeverAction m_LeverAction;
    [SerializeField]
    private AudioSource m_Source;

    [SerializeField]
    public Vector3 m_ReachPos;


    [Header("Gizmo Setting")]

    [SerializeField]
    private bool m_ShowGizmo;

    [SerializeField, Range(0, 5)]
    private float m_SizeOfGizmo;



    private Vector3 m_StartPos;

    private bool IsOnAction;


    private float m_leverCurrentValue;

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
        m_Source = GetComponent<AudioSource>();

    }

    private void Start()
    {
        m_StartPos = this.transform.position;
    }





    private void UpdateDoor(float leverAction)
    {

        this.transform.position = Vector3.Lerp(m_StartPos, m_StartPos + m_ReachPos, m_leverCurrentValue);
        
        if(!m_Source.isPlaying || this.transform.position == m_StartPos)
        m_Source.PlayOneShot(m_Source.clip);

    }


    private void Update()
    {
        if (!m_LeverAction.PlayerInAction) 
        {
          m_Source.Stop();
        }            
    }




}
