using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class UpdateNavMesh : MonoBehaviour
{
    private NavMeshSurface m_MeshSurface;

    
    private void Awake()
    {
       
        m_MeshSurface = GetComponent<NavMeshSurface>();
    }

 
    private void LateUpdate()
    {
        m_MeshSurface.BuildNavMesh();

    }

}
