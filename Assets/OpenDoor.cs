using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Animator m_Animator;

    public void DoorStatus(float callBack) 
    {
        m_Animator.SetFloat("Status", callBack);
    }


    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
