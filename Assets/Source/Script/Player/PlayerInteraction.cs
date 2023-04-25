    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInteraction))]
public class PlayerInteraction : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerThrowEnemy m_PlayerThrowEnemy;
    private PlayerPushBox m_PlayerPushBox;



    [SerializeField , Range(1,3)]
    private float PlayerInteractionRadius = 1;

    [SerializeField]
    private bool m_ShowInteractionRadius;

    [SerializeField]
    private Transform m_PlayerTransform;


    [SerializeField]
    private PlayerInput m_PlayerInput;



    [SerializeField]
    private Vector3 m_OffsetVector = Vector3.up;

    private void OnDrawGizmos()
    {
        if (m_ShowInteractionRadius) 
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(m_PlayerTransform.position + m_OffsetVector, PlayerInteractionRadius);  
        }
    }   

    private void Awake()
    {
        m_PlayerThrowEnemy = gameObject.GetComponent<PlayerThrowEnemy>();
        m_PlayerInput = GetComponent<PlayerInput>();
        m_PlayerPushBox = GetComponent<PlayerPushBox>();
    }



    // Lever Out
    public void OnChangeMap(InputAction.CallbackContext _callbackContext) 
    {
        if (_callbackContext.performed)
            m_PlayerInput.SwitchCurrentActionMap("GamePlay");
    }


    public void OnInteraction(InputAction.CallbackContext _callbackContext) 
    {
        if (_callbackContext.performed) 
        {
            Collider[] col = Physics.OverlapSphere(m_PlayerTransform.position + m_OffsetVector, PlayerInteractionRadius);
            
            

            foreach (Collider item in col)
            {
               
                if (item.gameObject.TryGetComponent<InteractableObject>(out InteractableObject interactableObject))
                {
                   
                    interactableObject.m_OnInteraction?.Invoke();

                   //if is levier do than return lever is 1st preiority
                    if(interactableObject is LevierIntercation) 
                    {
                        m_PlayerInput.SwitchCurrentActionMap("Lever");
                        return;
                    }

                    if(interactableObject is BoxInteraction) 
                    {
                        m_PlayerPushBox.GetBox(interactableObject, m_PlayerTransform.position);
                        return;
                    }

                    if(interactableObject is EnemyPickable) 
                    {
                        m_PlayerThrowEnemy.GetEnemy(interactableObject);
                        return;
                    }


                    
                }

                    

            }
        }

     

    }





}
