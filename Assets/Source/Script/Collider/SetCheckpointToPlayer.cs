using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SetCheckpointToPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private DimensionScript.Dimension m_Dimension;


    [SerializeField]
    private Transform m_positionEnemyToSpawn;




    [SerializeField]
    private bool m_DrawGizmo;

    [SerializeField]
    private float m_sizeGizmo;
    private void OnDrawGizmos()
    {
        if(m_DrawGizmo)
        {
            m_positionEnemyToSpawn = transform.GetChild(0);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(m_positionEnemyToSpawn.position, m_sizeGizmo);
        }
    }


    private void OnValidate()
    {
        if(m_Dimension == DimensionScript.Dimension.Normal) 
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -DimensionScript.DimensionSize / 2f);
        }
        else 
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, DimensionScript.DimensionSize / 2f);
        }
    }

    private void Awake()
    {
        if (m_Dimension == DimensionScript.Dimension.Normal)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -DimensionScript.DimensionSize / 2f);
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, DimensionScript.DimensionSize / 2f);
        }
        

      

        m_positionEnemyToSpawn = transform.GetChild(0);
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.parent.TryGetComponent<PlayerStatus>(out PlayerStatus status)) 
        {
            
            
                status.PlayerCurrentCheckpoint = this.transform.position;
                status.EnemyRespownCheckpoint = m_positionEnemyToSpawn.position;
            

        }
    }
}
