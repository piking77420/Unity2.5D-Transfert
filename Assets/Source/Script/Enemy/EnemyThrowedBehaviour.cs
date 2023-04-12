using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyThrowedBehaviour : MonoBehaviour
{
    // Start is called before the first frame update



    [SerializeField]
    public bool Is_Throwed;


    private void OnCollisionEnter(Collision collision)
    {
       
        if(Is_Throwed)
        if(collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb) && collision.gameObject.TryGetComponent<Collider>(out Collider coll)) 
        {
            coll.isTrigger = false;
            rb.useGravity = true;
            Destroy(gameObject);
        }




    }




    private void Awake()
    {
    
    }

    void Start()
    {
        
    }







    // Update is called once per frame
    void Update()
    {


      
    }
}
