using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;


public class test : MonoBehaviour
{
    // Start is called before the first frame update
    private void Update()
    {
        
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
