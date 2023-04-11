    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInteraction))]
public class PlayerInteraction : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerThrowEnemy m_PlayerThrowEnemy;


    [SerializeField , Range(1,3)]
    private float PlayerInteractionRadius = 1;

    [SerializeField]
    private bool m_ShowInteractionRadius;


    private void OnDrawGizmos()
    {
        if (m_ShowInteractionRadius) 
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(gameObject.transform.position, PlayerInteractionRadius);  
        }
    }

    private void Awake()
    {
        m_PlayerThrowEnemy = gameObject.GetComponent<PlayerThrowEnemy>();
    }

    public void OnInteraction(InputAction.CallbackContext _callbackContext) 
    {
        if (_callbackContext.performed) 
        {
            Collider[] col = Physics.OverlapSphere(transform.position, PlayerInteractionRadius);
            
            

            foreach (Collider item in col)
            {
               
                if (item.gameObject.TryGetComponent<InteractableObject>(out InteractableObject interactableObject))
                {
                   
                    interactableObject.m_OnInteraction?.Invoke();


                    m_PlayerThrowEnemy.GetEnemy(interactableObject);

                    
                }

                    

            }
        }

     

    }





}
