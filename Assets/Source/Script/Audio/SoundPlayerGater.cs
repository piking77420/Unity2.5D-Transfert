using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using static AudioManagers;
using static Unity.VisualScripting.Member;

public class SoundPlayerGater : MonoBehaviour
{
    public enum PlayerSoundOutPut 
    {
        PlayerVoice,PlayerJump,PlayerImpact
    }


    private CheckIsGround m_CheckIsGrounded;

    [SerializeField]
    private AudioSource[] m_PlayerAuduioSource;


    FootStepDistance[] PlayerFoot;


    [SerializeField]
    private bool m_IsGhost;



    private void Awake()
    {
        if (m_IsGhost)
            return;

        m_CheckIsGrounded = GetComponentInParent<CheckIsGround>();
        m_CheckIsGrounded.OnImpact.AddListener(OnplayerImpactGround);
        PlayerFoot = GetComponentsInChildren<FootStepDistance>();
        m_PlayerAuduioSource = GetComponents<AudioSource>();
    }


    public void PlayerFootStep(int Source)
    {
        if (m_IsGhost)
            return;

        AudioManagers.SourceFrom from = (AudioManagers.SourceFrom)Source;



        foreach (var item in PlayerFoot)
        {
            item.PlayFootStep(from);
        }
    }

    public void OnPlayerStartJump() 
    {
        if (m_IsGhost)
            return;




        AudioManagers.BiomeStat currentBiome = PlayerFoot[0].GetBiomeCollider(); ;

        AudioManagers.SourceFrom flight = AudioManagers.SourceFrom.PlayerFlight;
        AudioManagers.SourceFrom voice = AudioManagers.SourceFrom.PlayerVoice;



        AudioManagers.instance.PlayAudioAt(voice, AudioManagers.BiomeStat.Interior, m_PlayerAuduioSource[(int)PlayerSoundOutPut.PlayerVoice]);
        AudioManagers.instance.PlayAudioAt(flight, currentBiome, m_PlayerAuduioSource[(int)PlayerSoundOutPut.PlayerJump]);
        m_PlayerAuduioSource[(int)PlayerSoundOutPut.PlayerJump].PlayOneShot(m_PlayerAuduioSource[(int)PlayerSoundOutPut.PlayerVoice].clip);
        m_PlayerAuduioSource[(int)PlayerSoundOutPut.PlayerJump].PlayOneShot(m_PlayerAuduioSource[(int)PlayerSoundOutPut.PlayerJump].clip);

    }

    private void OnplayerImpactGround() 
    {

        if (m_IsGhost)
            return;

        AudioManagers.SourceFrom Impact = AudioManagers.SourceFrom.PlayerImpact;

        AudioManagers.BiomeStat currentBiome = PlayerFoot[0].GetBiomeCollider();

        AudioManagers.instance.PlayAudioAt(Impact, currentBiome, m_PlayerAuduioSource[(int)PlayerSoundOutPut.PlayerImpact]);


        m_PlayerAuduioSource[(int)PlayerSoundOutPut.PlayerImpact].PlayOneShot(m_PlayerAuduioSource[(int)PlayerSoundOutPut.PlayerImpact].clip);
    }




    private void OnPlayerEffort() 
    {
        if (m_IsGhost)
            return;

        AudioManagers.SourceFrom Source = AudioManagers.SourceFrom.FemaleEffort;

        AudioManagers.BiomeStat currentBiome = BiomeStat.Interior;

        AudioManagers.instance.PlayAudioAt(Source, currentBiome, m_PlayerAuduioSource[(int)PlayerSoundOutPut.PlayerImpact]);
        m_PlayerAuduioSource[(int)PlayerSoundOutPut.PlayerImpact].PlayOneShot(m_PlayerAuduioSource[(int)PlayerSoundOutPut.PlayerImpact].clip);
    }

}
