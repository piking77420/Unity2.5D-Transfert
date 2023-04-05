using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpwaner : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public GameObject Effect;


    [SerializeField]
    private Vector3 Distance;


    [SerializeField]
    private bool m_ShowSpawnPos;


   

    private void OnDrawGizmos()
    {
        if (m_ShowSpawnPos) 
        {
            Vector3 posSwapwn = gameObject.transform.position;
            posSwapwn.x += Distance.x;
            posSwapwn.y += Distance.y;
            posSwapwn.z -= Distance.z;

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(posSwapwn, 0.2f);
        }
    }


    public void SpawnEffect() 
    {
        Vector3 posSwapwn = gameObject.transform.position;
        posSwapwn.x += Distance.x;
        posSwapwn.y += Distance.y;
        posSwapwn.z -= Distance.z;

       Instantiate(Effect, posSwapwn, Effect.transform.rotation,gameObject.transform);
    }
 
}
