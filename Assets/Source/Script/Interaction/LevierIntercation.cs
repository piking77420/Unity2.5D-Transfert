using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevierIntercation : InteractableObject
{
    // Start is called before the first frame update
    [SerializeField]
    private Animator m_Animator;


    [SerializeField]
    private bool m_LeverStatus;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    void Start()
    {
        m_OnInteraction.AddListener(LeverAnimation);
    }

    public void LeverAnimation() 
    {
        m_LeverStatus = !m_LeverStatus;
        // setBool
    }

   
}
