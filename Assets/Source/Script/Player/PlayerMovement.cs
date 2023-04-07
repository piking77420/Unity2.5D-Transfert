using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody m_Rigidbody;


    [SerializeField, Range(0, 100)]
    private float m_Speed;

    [SerializeField, Range(1,3)]
    private float m_SpeedMultiplactorOnRunning;

    [SerializeField]
    public Vector3 movement;

 


    public void OnMovement(InputAction.CallbackContext _context) 
    {
        
        Vector3 movementInput =  new Vector3(_context.ReadValue<float>(),0,0);

        movement = movementInput;
       
            
    }




    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }



    private void PlayerRunning(ref Vector3 currentMovment ) 
    {
        if (movement.x == 1 || movement.x == -1)
        {
            currentMovment *= m_SpeedMultiplactorOnRunning;

        }
    }

 
    // Update is called once per frame
    void Update()
    {
        Vector3 currentMovment = movement * m_Speed;

        PlayerRunning(ref currentMovment);  
        m_Rigidbody.velocity = new Vector3(currentMovment.x , m_Rigidbody.velocity.y , currentMovment.z);
     
    }
}
