using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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





    public void OnleverAction(InputAction.CallbackContext _callbackContext) 
    {
        Debug.Log("sqd");

        if (_callbackContext.performed) 
        {
            Debug.Log(_callbackContext.ReadValue<float>()) ;
        }
    }







    private void Awake()
    {
        m_LevierIntercation = GetComponent<LevierIntercation>();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
