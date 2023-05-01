using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class UpdateNavMesh : MonoBehaviour
{
    private NavMeshSurface[] m_MeshSurfaces;

    public DimensionScript m_Dimension;


    private DimensionScript.Dimension m_LastUpdateD;

    private void Awake()
    {
        m_Dimension = FindObjectOfType<DimensionScriptPlayer>();
          m_LastUpdateD = m_Dimension.CurrentDimension;
         m_MeshSurfaces = GetComponents<NavMeshSurface>();
    }


    

 
    private void LateUpdate()
    {
        if(m_Dimension.CurrentDimension != m_LastUpdateD) 
        {
            foreach(NavMeshSurface surface in m_MeshSurfaces)
            {
                surface.BuildNavMesh();
            }
            m_LastUpdateD = m_Dimension.CurrentDimension;
        }
    }

}
