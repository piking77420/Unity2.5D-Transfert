using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeActionMap : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private PlayerInput m_PlayerInput;

    public void ChangeActionMapToLever() 
    {
        m_PlayerInput.SwitchCurrentActionMap("Lever"); 
    }

  
    public void OnQuitLeverActionMap(InputAction.CallbackContext _callbackContext) 
    {
        if (_callbackContext.performed) 
        {
            m_PlayerInput.SwitchCurrentActionMap("Gameplay");
        }
    }

    


    private void Awake()
    {
        m_PlayerInput = GetComponent<PlayerInput>();    
    }


}
