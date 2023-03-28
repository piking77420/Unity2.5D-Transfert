using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody m_Rigidbody;


    [SerializeField, Range(0, 100)]
    private float m_Speed;

    [SerializeField]
    public Vector3 movement;


    public void OnMovement(InputAction.CallbackContext _context) 
    {
        
        Vector3 movementInput =  new Vector3(_context.ReadValue<Vector2>().x ,0, _context.ReadValue<Vector2>().y );
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

    // Update is called once per frame
    void Update()
    {
        Vector3 currentMovment = movement * m_Speed;
        m_Rigidbody.velocity = new Vector3(currentMovment.x , m_Rigidbody.velocity.y , currentMovment.z);
     
    }
}
