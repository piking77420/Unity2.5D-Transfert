using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhostDetection : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField,Range(0.5f,2)]
    private float GhostDetectionRadius;

    [SerializeField]
    private MeshRenderer m_Renderer;

    [SerializeField]
    private bool ShowGizmo;


    [SerializeField,Range(10,100)]
    private float m_GhostIntensity;



    private void OnDrawGizmos()
    {
        if(ShowGizmo)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, GhostDetectionRadius);
        }
    }





    private void ChangeColorFromStatus(bool value)
    {
        m_Renderer.material.SetFloat("_Depth_Edge",13);
        m_Renderer.material.SetFloat("_Fresnel_Power", 3);
        m_Renderer.material.SetFloat("_Fill", 0.2f);



        if (!value)
        {
            Color color = Color.red;

            m_Renderer.material.SetColor("_Color", color * m_GhostIntensity * 1.2f);
        }
        else
        {   
            Color color = Color.white;

            m_Renderer.material.SetColor("_Color", color * m_GhostIntensity );
        }
    }




    public bool IsCanPlayerTranslate() 
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, GhostDetectionRadius);
        
      


        foreach(Collider collider in colliders) 
        {
            if(collider.gameObject != gameObject) 
            {
                ChangeColorFromStatus(false);
                return false;
            }
        }

        ChangeColorFromStatus(true);
        return true ;




    }

    private void Awake()
    {
        m_Renderer = GetComponent<MeshRenderer>();
    }


    private void Update()
    {
        IsCanPlayerTranslate(); 
    }

}
