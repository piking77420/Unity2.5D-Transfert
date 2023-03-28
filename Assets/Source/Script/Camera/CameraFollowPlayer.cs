using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Transform m_PlayerTransform;
    [Space]

    [Header("Camera Setting")]

    [SerializeField,Range(0, 20)]
    private float m_DistanceX;

    [SerializeField,Range(0, 20)]
    private float m_Height;
   
    [SerializeField,Range(0,20)]
    private float m_DistanceZ;




    private void OnValidate()
    {
        this.transform.position = m_PlayerTransform.position + new Vector3(-m_DistanceX , m_Height , -m_DistanceZ);
        transform.LookAt(m_PlayerTransform);

    }




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        this.transform.position = m_PlayerTransform.position + new Vector3(-m_DistanceX , m_Height , -m_DistanceZ);

        transform.LookAt(m_PlayerTransform);
        
        

    }
}
