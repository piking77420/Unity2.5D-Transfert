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


    [SerializeField]
    private float m_LeverValueStatue;


    [SerializeField,Tooltip("EVENT WHO IS PLAY EACH TIME BY FRAME")]
    public UnityEvent OnAccomplish;




    public void OnleverAction(InputAction.CallbackContext _callbackContext) 
    {

        if (_callbackContext.performed) 
        {
            m_LeverValueStatue = _callbackContext.ReadValue<float>();


            m_Animator.SetFloat("Status", m_LeverValueStatue);


        }
    }







    private void Awake()
    {
        m_LevierIntercation = GetComponent<LevierIntercation>();
        m_Animator = GetComponent<Animator>();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_LeverValueStatue >= 1f) 
        {
            OnAccomplish?.Invoke();
        }
    }
}
