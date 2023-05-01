using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LevierIntercation : InteractableObject
{
    // Start is called before the first frame update

    [SerializeField]
    private PlayerInput m_PlayerInput;
    [SerializeField]
    private LeverAction m_LeverAction;



    private void ChangeLeverActionMap() 
    {
        m_PlayerInput.enabled = true;
        m_LeverAction.IsAcomplish = false;
    }

    public void QuitMap(InputAction.CallbackContext _callbackContext)
    {
        if (_callbackContext.performed) 
        {
            m_PlayerInput.enabled = false;
        }
    }


    private void Awake()
    {
        m_PlayerInput = GetComponent<PlayerInput>();
        m_PlayerInput.enabled = false;
        m_LeverAction = GetComponent<LeverAction>();
    }

    void Start()
    {
        m_OnInteraction.AddListener(ChangeLeverActionMap);
    }



}
