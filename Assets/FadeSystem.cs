using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeSystem : MonoBehaviour
{
    [SerializeField]
    private Image m_FadeImage;

    [SerializeField]
    private Animator m_Animator;

    private void Awake()
    {
        m_FadeImage= GetComponent<Image>();
        m_Animator = GetComponent<Animator>();  


    }
    public void OnEndLevel()
    {
        m_Animator.SetTrigger("End");
    }

    public void OnStartLevel() 
    {
        m_Animator.SetTrigger("Start");
    }

    public void OnDeathPlayer() 
    {
        m_Animator.SetTrigger("OnDeath");

    }


 




    // Start is called before the first frame update
    void Start()
    {
        OnStartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
