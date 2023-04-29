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




    private void OnDrawGizmos()
    {
        if (m_ShowGizmo)
        {
            Gizmos.color = Color.blue;


            Gizmos.DrawWireSphere(m_ReachPos + this.transform.position, m_SizeOfGizmo);
        }
    }


    private void StopDoorSound() 
    {
        m_Source.Stop();
    }

    private void Awake()
    {

        m_LeverAction = transform.parent.parent.GetComponentInChildren<LeverAction>();
        m_LeverAction.OnAccomplish.AddListener(OpendDoor);
        m_LeverAction.OnNonAccomplish.AddListener(CloseDoor);
        m_LeverAction.OnQuitLever.AddListener(StopDoorSound);
        m_Source = GetComponent<AudioSource>();

    }

    private void Start()
    {
        m_StartPos = this.transform.position;
    }



    private void CloseDoor(float value) 
    {
        if(value == 0) 
        {
            return;
        }

        if(value > 0) 
        {

            this.transform.position = Vector3.Lerp(m_StartPos, m_StartPos + m_ReachPos, value);
            Debug.Log("close");

            if (!m_Source.isPlaying && m_LeverAction.m_LeverReadValue != 0)
            {
                Debug.Log("open");

                m_Source.PlayOneShot(m_Source.clip);
            }
        }

    }




    private void OpendDoor(float value)
    {


        if (value <= 1f)
        {

            this.transform.position = Vector3.Lerp(m_StartPos, m_StartPos + m_ReachPos, value);

            if (!m_Source.isPlaying && m_LeverAction.m_LeverReadValue != 0)
            {
                m_Source.PlayOneShot(m_Source.clip);
            }
           


        }




    }


    private void Update()
    {
          if(m_LeverAction.m_LeverReadValue == 0)
            m_Source.Stop();
    }




}
