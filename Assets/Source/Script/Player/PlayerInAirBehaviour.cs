using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirBehaviour : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField]
    private PlayerMovement m_movement;

    [SerializeField]
    private CheckIsGround m_CheckIsGround;


    [SerializeField]
    private Rigidbody m_Rigidbody;


    [SerializeField]
    private bool m_OnWall;


    private void Awake()
    {
        m_CheckIsGround = GetComponent<CheckIsGround>();
        m_movement = GetComponent<PlayerMovement>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnCollisionStay(Collision collision)
    {
        if(!m_CheckIsGround.isGrounded)
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            if(Vector3.Angle(collision.contacts[i].normal,Vector3.up) >= 90f) 
            {
                m_movement.movement.Set(0, m_movement.movement.y, 0);
                break;
              
            }
        }
    }



  
}
