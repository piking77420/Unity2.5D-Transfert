using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(PlayerMovement))]
public class PlayerDash : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private PlayerMovement m_PlayerMovement;

    [SerializeField]
    private float m_DashStrenght;

    [SerializeField]
    private float m_DashCoolDown;

    [SerializeField]
    private float m_DashTime;


    private float DashMaxCoolDown;
    private bool IsDashing;




    [SerializeField]
    private Vector3 m_DashingVector;

    public void OnDash(InputAction.CallbackContext callbackContext) 
    {
        if (!IsDashing) 
        {

            IsDashing = true;
        }
    }


    IEnumerator DashCouroutine() 
    {
        m_Rigidbody.useGravity = false;
        Vector3 temporayVector = m_Rigidbody.velocity;
        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.AddForce(m_PlayerMovement.movement * m_DashStrenght, ForceMode.Impulse);
    
        yield return new WaitForSeconds(m_DashTime);
        m_Rigidbody.velocity = temporayVector;
        m_Rigidbody.useGravity = true;


        IsDashing = false;

    }

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_PlayerMovement = GetComponent<PlayerMovement>();
        DashMaxCoolDown = m_DashCoolDown;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

   

    // Update is called once per frame
    void Update()
    {
        if (IsDashing)
        {
            m_DashingVector = m_PlayerMovement.movement * m_DashStrenght;
            m_Rigidbody.velocity += m_DashingVector;
            StartCoroutine(DashCouroutine());
        }

       
    }
}
