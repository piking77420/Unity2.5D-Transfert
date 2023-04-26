using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField,Range(0,10)]
    private float m_RaycastDistance;

    [SerializeField]
    private LayerMask m_LayerMask;

    [SerializeField]
    public UnityEvent OnImpact;

    bool CheckCollsion(Collision collision)
    {
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            float getangle = Vector3.Angle(collision.contacts[i].normal, Vector3.up);


            if (getangle < DesiredAngle)
            {

                OnImpact.Invoke();

                return true;
            }
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
        m_GraphicIsGrounded = transform.GetChild(0).transform;
    }

    // Start is called before the first frame update
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


        if (!isGrounded)
        {
            Ray r = new Ray(m_GraphicIsGrounded.position, Vector3.down);
            if(Physics.Raycast(r, m_RaycastDistance, m_LayerMask)) 
            {
                isGrounded = true;
            }
        }


    }



}
