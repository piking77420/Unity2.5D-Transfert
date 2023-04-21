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
    EnemyPickable enemyPickable;
    private void OnTriggerExit(Collider other)
    {


        // TO DO 
        if(other.gameObject.TryGetComponent<EnemyPickable>(out enemyPickable)) 
        {
            //m_rb.useGravity = true;
            m_collid.isTrigger = false;
            Destroy(enemyPickable.transform.root.gameObject);
        }
    }




    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_collid = GetComponent<Collider>();
        m_collid.isTrigger = true;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
