using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LeverAction : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private LevierIntercation m_LevierIntercation;

    [SerializeField]
    private Animator m_Animator;


    [SerializeField,Range(0f,1f)]
    private float m_LeverValueStatue;


    [SerializeField,Tooltip("EVENT PLAY EACH FRAME")]
    public UnityEvent<float> OnAccomplish;


  


    [SerializeField,Range(0,10)]
    private float OpenDoorStreght;

    private float LeverPlayerStrenght;


    [SerializeField,Range(0,2)]
    private float DecreaseStreght;



    [SerializeField]
    private bool IsAcomplish;
    [SerializeField]
    private bool PlayerInAction;

    [SerializeField]
    public bool IsObstruct;


    public void OnQuit(InputAction.CallbackContext _callbackContext) 
    {
        if (_callbackContext.performed) 
        {
            PlayerInAction = false;
        }

    }


    public void OnleverAction(InputAction.CallbackContext _callbackContext) 
    {



        switch (_callbackContext.phase) 
        {
            case InputActionPhase.Started:
                PlayerInAction = true;   
                break;
            case InputActionPhase.Performed:
                LeverPlayerStrenght = _callbackContext.ReadValue<float>();
                break;
            case InputActionPhase.Canceled:
                PlayerInAction = false;
                break;

        }
    }







    private void Awake()
    {
        m_LevierIntercation = GetComponent<LevierIntercation>();
        m_Animator = GetComponent<Animator>();

    }




    private void OpenDoor() 
    {
        // Had This to avoid when doing left right realy fast that opening the door to quciky
        if(LeverPlayerStrenght >= 1f)
        {
            LeverPlayerStrenght = 0.5f;
        }

        if (PlayerInAction && !IsAcomplish)
        {
            
            m_LeverValueStatue += LeverPlayerStrenght * Time.deltaTime * OpenDoorStreght;

        }

    }


    private void CloseDoor() 
    {

        if (IsObstruct) 
        {
            
            return;
        }

        if (!PlayerInAction && !IsAcomplish && m_LeverValueStatue >= 0)
        {
           // m_Animator.SetFloat("Status", m_LeverValueStatue);

            m_LeverValueStatue -= Time.deltaTime * DecreaseStreght;


        }
    }

    private void CheckStatue() 
    {
        if (m_LeverValueStatue <= 1f)
        {
            OnAccomplish?.Invoke(m_LeverValueStatue);
        }
        else
        {
            IsAcomplish = true;
        }
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        OpenDoor();
        CloseDoor();

        CheckStatue();
        m_Animator.SetFloat("Status", m_LeverValueStatue);
    }

    
}
