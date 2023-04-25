using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerGhostDetection : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField,Range(0.5f,2)]
    private float GhostDetectionRadius;



    [SerializeField]
    private Transform m_HispTransform;

    [SerializeField]
    private SkinnedMeshRenderer[] m_Renderers;

    [SerializeField]
    private bool ShowGizmo;


    [SerializeField,Range(10,100)]
    private float m_GhostIntensity;


    [SerializeField]
    private Vector3 m_DetectionPos;

    [SerializeField]
    private LayerMask m_LayerMask;




    private void OnDrawGizmos()
    {
        if(ShowGizmo)
        {
            Gizmos.color = Color.yellow;
            Vector3 posDetection = m_HispTransform.position  + m_DetectionPos;
            Gizmos.DrawWireSphere(posDetection, GhostDetectionRadius);
        }
    }





    private void ChangeColorFromStatus(bool value)
    {

        foreach(var renderer in m_Renderers) 
        {
            renderer.material.SetFloat("_Depth_Edge", 13);
            renderer.material.SetFloat("_Fresnel_Power", 3);
            renderer.material.SetFloat("_Fill", 0.2f);

            

            if (!value)
            {
                Color color = Color.red;
                renderer.material.SetColor("_Color", color * m_GhostIntensity * 1.2f);
            }
            else
            {
                Color color = Color.white;

                renderer.material.SetColor("_Color", color * m_GhostIntensity);
            }
        }

        
    }







    public bool IsCanPlayerTranslate() 
    {
        Collider[] colliders = Physics.OverlapSphere(m_HispTransform.position + m_DetectionPos, GhostDetectionRadius);
        
      


        foreach(Collider collider in colliders) 
        {

            if(collider.gameObject != gameObject && collider.gameObject.layer == m_LayerMask) 
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
        m_Renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }


    private void Update()
    {
        IsCanPlayerTranslate(); 
    }

}
