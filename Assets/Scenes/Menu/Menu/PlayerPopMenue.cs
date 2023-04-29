using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPopMenue : MonoBehaviour
{

    [SerializeField]
    private PlayerInput m_PlayerInput;



    private const string Menue = "Menue";


    private string m_actionMapBuffer;

    private void Awake()
    {
        m_PlayerInput = GetComponent<PlayerInput>();
    }


    public void OnBackToGame(InputAction.CallbackContext _callbackContext)
    {
        if (_callbackContext.performed)
        {
            m_PlayerInput.SwitchCurrentActionMap(m_actionMapBuffer);
            m_actionMapBuffer = null;
        }
    }



    public void OnPopPause(InputAction.CallbackContext _callbackContext) 
    {
        if(_callbackContext.performed) 
        {
            m_actionMapBuffer = m_PlayerInput.currentActionMap.name;
            m_PlayerInput.SwitchCurrentActionMap(Menue);
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
