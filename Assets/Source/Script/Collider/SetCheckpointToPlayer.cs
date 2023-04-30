using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCheckpointToPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private DimensionScript.Dimension m_Dimension;




    private int m_CheckpointIndex;

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
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.parent.TryGetComponent<PlayerStatus>(out PlayerStatus status)) 
        {
            if(status.currentCheckpointIndex <= m_CheckpointIndex)
            {
                status.PlayerCurrentCheckpoint = this.transform.position;
                status.currentCheckpointIndex = m_CheckpointIndex;

            }

        }
    }
}
