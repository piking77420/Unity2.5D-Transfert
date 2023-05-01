using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TangibleOnEnemyInside : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Rigidbody m_rb;

    [SerializeField]
    private Collider m_collid;

    [SerializeField]
    private Material m_Material;

    [SerializeField]
    EnemyPickable enemyPickable;

    [SerializeField]
    private float m_TimeForBeTangible;

    [SerializeField,Range(0,1)]
    private float m_StarValue;

    private float m_TimeForTangibleMax;

    private void OnTriggerExit(Collider other)
    {


        if(other.gameObject.TryGetComponent<EnemyPickable>(out enemyPickable)) 
        {
            //m_rb.useGravity = true;
            m_collid.isTrigger = false;
            Destroy(enemyPickable.transform.root.gameObject);
            m_Material.SetFloat("_Health", m_StarValue);
        }
    }




    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_collid = GetComponent<Collider>();
        m_collid.isTrigger = true;
        m_TimeForTangibleMax = m_TimeForBeTangible;
        m_Material = GetComponent<MeshRenderer>().material;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_collid.isTrigger == false) 
        {
            m_TimeForBeTangible -= Time.deltaTime;
            float materialValue = m_Material.GetFloat("_Health");

            if (materialValue > 0) 
            {
                m_Material.SetFloat("_Health", materialValue - (Time.deltaTime/ m_TimeForTangibleMax));
            }


            if (m_TimeForBeTangible < 0) 
            {
                m_collid.isTrigger = true;
                m_TimeForBeTangible = m_TimeForTangibleMax;
            }

        }
    }
}
