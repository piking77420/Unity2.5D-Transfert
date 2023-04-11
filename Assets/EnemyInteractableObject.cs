using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteractableObject : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Rigidbody m_rb;

    [SerializeField]
    private Collider m_collid;



  
    private void OnTriggerExit(Collider other)
    {

        if(other.gameObject.TryGetComponent<EnemyPickable>(out EnemyPickable enemyPickable)) 
        {
            m_rb.useGravity = true;
            m_collid.isTrigger = false;
        }
    }




    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_collid = GetComponent<Collider>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
