using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class UpdateNavMesh : MonoBehaviour
{
    private NavMeshSurface[] m_MeshSurfaces;

    
    private void Awake()
    {
       
        m_MeshSurfaces = GetComponents<NavMeshSurface>();
    }

 
    private void LateUpdate()
    {
        foreach (var item in m_MeshSurfaces)
        {
            item.BuildNavMesh();
        } 
    }

}
