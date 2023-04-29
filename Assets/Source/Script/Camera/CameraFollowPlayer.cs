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

    [Range(0, 50)]
    public float m_DistanceX;

    [Range(0, 50)]
    public float m_Height;
   
    [Range(0,20)]
    public float m_DistanceZ;




    [SerializeField, Range(0, 10)]
    private float m_FollowSpeed;


    private void OnValidate()
    {
        this.transform.position = m_PlayerTransform.position + new Vector3(-m_DistanceX , m_Height , -m_DistanceZ);
        transform.LookAt(m_PlayerTransform);

    }




    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector3 newPos = m_PlayerTransform.position + new Vector3(-m_DistanceX, m_Height, -m_DistanceZ);
        Vector3 smoothedPos = Vector3.Lerp(transform.position, newPos, m_FollowSpeed * Time.deltaTime);

        this.transform.position = smoothedPos;

        transform.LookAt(m_PlayerTransform);
        
        

    }
}
