using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIsGround : MonoBehaviour
{

    [SerializeField]
    public bool isGrounded;


    [SerializeField,Range(0,180)]
    private float DesiredAngle;


    [Header("NbrOfCollision")]
    public List<Collider> colliders;
    [SerializeField]
    private int m_nbrOfColldier;



    bool CheckCollsion(Collision collision)
    {
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            float getangle = Vector3.Angle(collision.contacts[i].normal, Vector3.up);


            if (getangle < DesiredAngle)
            {
               
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
       


    }



}
