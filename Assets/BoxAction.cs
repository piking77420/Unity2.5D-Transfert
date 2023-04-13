using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoxAction : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private BoxInteraction m_BoxIntercation;



    private void OnBoxAction(InputAction.CallbackContext _callbackContext) 
    {

    }



    private void Awake()
    {
        m_BoxIntercation = GetComponent<BoxInteraction>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
