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

    private int m_CheckpointIndex;



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
        

        for (int i = 0; i < transform.parent.childCount; i++) 
        {
            if(transform.parent.GetChild(i) == this.transform) 
            {
                m_CheckpointIndex = i;
            }
        }

        m_positionEnemyToSpawn = transform.GetChild(0);
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.parent.TryGetComponent<PlayerStatus>(out PlayerStatus status)) 
        {
            if(status.currentCheckpointIndex <= m_CheckpointIndex)
            {
                status.PlayerCurrentCheckpoint = this.transform.position;
                status.currentCheckpointIndex = m_CheckpointIndex;
                status.EnemyRespownCheckpoint = m_positionEnemyToSpawn.position;
            }

        }
    }
}
