using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoyOnLoadScript : MonoBehaviour
{
    [SerializeField]
    public Object[] Array;

    private void Awake()
    {
        foreach (var obj in Array) 
        {
            DontDestroyOnLoad(obj);
        }
    }
}
