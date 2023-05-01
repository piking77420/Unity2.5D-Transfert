using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CheatManagers : MonoBehaviour
{


    [SerializeField]
    private PlayerInput m_PlayerInput;

    


    private void Awake()
    {
        m_PlayerInput = GetComponent<PlayerInput>();
    }



    //Athenais
    private void ChangeToVillage(InputAction.CallbackContext _callbackContext) 
    {
        if (_callbackContext.performed) 
        {

        }
    }

    // ETHAN
    private void ChangeToVillage2(InputAction.CallbackContext _callbackContext)
    {
        if (_callbackContext.performed)
        {

        }
    }

    // Leonard
    private void ChangeToCountry(InputAction.CallbackContext _callbackContext)
    {
        if (_callbackContext.performed)
        {

        }
    }

    //Leo
    private void ChangeToForest(InputAction.CallbackContext _callbackContext)
    {
        if (_callbackContext.performed)
        {

        }
    }





    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
