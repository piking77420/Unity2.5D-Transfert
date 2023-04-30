using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngineInternal;

public class CheckIsGround : MonoBehaviour
{

    [SerializeField]
    public bool isGrounded;


    private Transform m_GraphicIsGrounded;

    [SerializeField,Range(0,180)]
    private float DesiredAngle;


    [Header("NbrOfCollision")]
    public List<Collider> colliders;
    [SerializeField]
    private int m_nbrOfColldier;

    


    [SerializeField]
    public UnityEvent OnImpact;

    private Collider m_collider;



    private bool CheckCollisionAngle(Vector3 Normal) 
    {
        float getangle = Vector3.Angle(Normal, Vector3.up);


        if (getangle < DesiredAngle)
        {

            OnImpact.Invoke();

            return true;
        }

        return false;
    }



    bool CheckCollsion(Collision collision)
    {
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            CheckCollisionAngle(collision.contacts[i].normal);
            return true;
        }

        return false;

    }
    private void OnCollisionExit(Collision collision)
    {

        colliders.Remove(collision.collider);
    }

    private void OnCollisionEnter(Collision collision)
    {



        colliders.Add(collision.collider);


        if (!isGrounded)
        {
            isGrounded = CheckCollsion(collision);
        }
    }

    



    private void Awake()
    {
        colliders = new List<Collider>();
        // geting player componeent in main graphics
        m_GraphicIsGrounded = transform.GetChild(0).transform;
        m_collider = m_GraphicIsGrounded.GetComponent<Collider>();

        if(m_collider == null) 
        {
            m_collider = GetComponent<Collider>();
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_nbrOfColldier = colliders.Count;

        if(colliders.Count <= 0) 
        {
            isGrounded = false;
        }

        Debug.DrawRay(m_GraphicIsGrounded.position, Vector3.down,Color.green);

        

        if (m_collider is CapsuleCollider && !isGrounded)
        {
            float extraDistance = 0.2f;   
            if(Physics.Raycast(m_collider.bounds.center, Vector3.down,out RaycastHit hit ,  m_collider.bounds.extents.y + extraDistance)) 
            {
                if(hit.collider != null) 
                {

                 //  isGrounded = CheckCollisionAngle(hit.normal);

                }
            }
        }


    }



}
