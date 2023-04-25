using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundOutpout : MonoBehaviour
{
    [SerializeField]
    private AudioManagers.SourceFrom SoundSourceType;

    [SerializeField]
    private PlayerMovement m_PlayerMovment;

    [SerializeField]
    private PlayerJump m_PlayerJump;

    [SerializeField]
    private PlayerPushBox m_PlayerPushBox;



    private void Awake()
    {
        m_PlayerJump= GetComponent<PlayerJump>();
        m_PlayerMovment =  GetComponent<PlayerMovement>();
        m_PlayerPushBox = GetComponent<PlayerPushBox>();

    }


    private void GetPlayerSoundState()
    {
        /*
       if(m_PlayerJump.IsJumping)
       {
            // Start Jump;  
            //SoundSourceType = AudioManagers.SourceFrom.
            return 
       }*/

        if(m_PlayerPushBox.m_CurrentBox != null) 
        {
            SoundSourceType = AudioManagers.SourceFrom.Box;
            return;
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerSoundState();
    }

   
}
