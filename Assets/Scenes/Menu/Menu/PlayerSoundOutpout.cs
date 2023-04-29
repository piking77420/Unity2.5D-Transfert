using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundOutpout : MonoBehaviour
{
    [SerializeField]
    public AudioManagers.SourceFrom SoundSourceType;

    [SerializeField]
    private PlayerMovement m_PlayerMovment;

    [SerializeField]
    private PlayerJump m_PlayerJump;

    [SerializeField]
    private PlayerPushBox m_PlayerPushBox;



    private void Awake()
    {
        m_PlayerJump= GetComponentInParent<PlayerJump>();
        m_PlayerMovment = GetComponentInParent<PlayerMovement>();
        m_PlayerPushBox = GetComponentInParent<PlayerPushBox>();

    }


    public void OnPlayerBox()
    {
        
    }

    public void OnPlayerJump()
    {

    }


    public void OnPlayerRun()
    {

    }


    public void OnPlayeWalk()
    {

    }



 
   
}
