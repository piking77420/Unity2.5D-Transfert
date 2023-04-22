using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class StopDoorOnBox : MonoBehaviour
{
    // Start is called before the first frame update


    [Header("Dependancies")]
    [SerializeField]
    private LeverAction m_LeverAction;





    private void Awake()
    {
        m_LeverAction = transform.root.GetComponentInChildren<LeverAction>();


    }




    private void OnTriggerEnter(Collider collision)
    {





        if (collision.TryGetComponent<BoxInteraction>(out BoxInteraction box))
        {
            m_LeverAction.IsObstruct = true;
        }

    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<BoxInteraction>(out BoxInteraction box))
        {
            m_LeverAction.IsObstruct = false;
        }

    }

}
