using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UpdateNavMesh : MonoBehaviour
{

    [SerializeField]
    private LeverAction m_LeverAction;

    private void Awake()
    {
        m_LeverAction = transform.root.GetComponentInChildren<LeverAction>();
    }


    private bool IsBox(Collider collision) 
    {
        return false;
    }


    private void OnTriggerEnter(Collider collision)
    {

        

        if (collision.TryGetComponent<NavMeshSurface>(out NavMeshSurface navSurface)) 
        {
          
           // navSurface.BuildNavMesh();    
        }

        if (collision.TryGetComponent<BoxInteraction>(out BoxInteraction box))
        {
             m_LeverAction.IsObstruct = true;
        }

    }

    private void OnTriggerExit(Collider collision)
    {


        /*

        if (collision.TryGetComponent<NavMeshSurface>(out NavMeshSurface navSurface))
        {
            Debug.Log("On quit ");

            ///NavSurface.BuildNavMesh();
        }*/

        if (collision.TryGetComponent<BoxInteraction>(out BoxInteraction box))
        {
            m_LeverAction.IsObstruct = false;
        }

    }

}
