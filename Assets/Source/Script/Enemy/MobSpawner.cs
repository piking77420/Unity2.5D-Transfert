using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject m_Prefab;


    [SerializeField]
    private GameObject m_CurrentEnemy;

    [SerializeField]
    private List<Vector3> m_ListOfWaypoint; 


    [SerializeField,Range(0,10)]
    private float m_TimerForSpawner;

    [SerializeField]
    private bool m_ShowGizmo;

    [SerializeField,Range(0,3)]
    private float m_GizmoSpawnPoint;



    private float m_CurrentTimer;

    private void OnDrawGizmos()
    {
        if (m_ShowGizmo)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, m_GizmoSpawnPoint);

            foreach (var item in m_ListOfWaypoint) 
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(item + this.transform.position, m_GizmoSpawnPoint);
            }

        }
    }

    private void Awake()
    {
        m_CurrentTimer = m_TimerForSpawner;
    }


    private IEnumerator SpawnTime() 
    {
        yield return new WaitForSeconds(m_TimerForSpawner);
        m_CurrentEnemy = Instantiate(m_Prefab,transform.position,Quaternion.identity) ;


        if(m_CurrentEnemy.TryGetComponent<EnemyPatrol>(out EnemyPatrol enemyPatrol)) 
        {
            enemyPatrol.SetPath(m_ListOfWaypoint,this);
        }


    }

    private void EnemySpawner() 
    {
        if (m_CurrentEnemy == null)
        {

            if (m_CurrentTimer > 0)
            {
                m_CurrentTimer -= Time.deltaTime;
            }

            if (m_CurrentTimer <= 0)
            {
                m_CurrentEnemy = Instantiate(m_Prefab, transform.position, Quaternion.identity);

                if (m_CurrentEnemy.GetComponentInChildren<EnemyPatrol>() && m_ListOfWaypoint.Count != 0 )
                {
                    m_CurrentEnemy.GetComponentInChildren<EnemyPatrol>().SetPath(m_ListOfWaypoint,this);
                }
                m_CurrentTimer = m_TimerForSpawner;
            }

        }
    }



    // Update is called once per frame
    void Update()
    {
        EnemySpawner();
    }
}
