using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UpdateNavMesh : MonoBehaviour
{

    [SerializeField]
    private Animator animator;



    private void OnTriggerEnter(Collider collision)
    {

        Debug.Log(collision.gameObject.name);


        if (collision.TryGetComponent<NavMeshSurface>(out NavMeshSurface navSurface)) 
        {
            Debug.Log("On enter ");
           // navSurface.BuildNavMesh();
        }else if (collision.TryGetComponent<BoxInteraction>(out BoxInteraction box)) 
        {
            GetComponentInParent<Animator>().enabled = false;
        }


    }

    private void OnTriggerExit(Collider collision)
    {




        if (collision.TryGetComponent<NavMeshSurface>(out NavMeshSurface navSurface))
        {
            Debug.Log("On quit ");

            ///NavSurface.BuildNavMesh();
        }
    }

}
