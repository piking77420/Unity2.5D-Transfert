    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInteraction))]
public class PlayerInteraction : MonoBehaviour
{
    // Start is called before the first frame update

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



    public void OnInteraction(InputAction.CallbackContext _callbackContext) 
    {
        if (_callbackContext.performed) 
        {
            Collider[] col = Physics.OverlapSphere(transform.position, PlayerInteractionRadius);
            
            

            foreach (Collider item in col)
            {
               
                if (item.gameObject.TryGetComponent<InteractableObject>(out InteractableObject interactableObject))
                {
                    Debug.Log("here");

                    interactableObject.m_OnInteraction?.Invoke();
                }
            }
        }

     

    }





}
