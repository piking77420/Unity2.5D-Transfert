using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhostDetection : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField,Range(1,2)]
    private float GhostDetectionRadius;

    [SerializeField]
    private bool ShowGizmo;

    private void OnDrawGizmos()
    {
        if(ShowGizmo)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, GhostDetectionRadius);
        }
    }



    public bool IsCanPlayerTranslate() 
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, GhostDetectionRadius);
        

        if(colliders.Length == 0) 
        {
            return true;
        }

        return false;
    }


}
