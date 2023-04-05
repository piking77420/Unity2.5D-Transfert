using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    // Start is called before the first frame update



    [SerializeField]
    protected Renderer m_Renderer;


    protected void Awake()
    {
        m_Renderer = GetComponent<Renderer>();
    }

    protected void Update()
    {
        
    }
    // Update is called once per frame

}
