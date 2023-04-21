using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEmptyTracker : MonoBehaviour
{
    [SerializeField]
    private Transform m_PlayerTransform;



    [SerializeField,Range(0,10)]
    private float m_FollwoSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, m_PlayerTransform.position, Time.deltaTime * m_FollwoSpeed);    
    }
}
