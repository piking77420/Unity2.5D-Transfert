using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEditor.Progress;
using static DimensionScript.Dimension;
using static AudioManagers;

public class AudioManagers : MonoBehaviour
{
    // Start is called before the first frame update


    // In Child Has to be in order
    public enum SourceFrom
    {
        WalkPlayer,
        MoveBox_Start,
        MoveBox_Move,
        MoveBox_End,
        Slime,
        TheEnemy,
        PlayerRun,
        PlayerFlight,
        PlayerVoice,
        PlayerImpact,
        FemaleEffort,
       
    };

    // In Child Has to be in order
    public enum BiomeStat
    {
        Interior,
        Village,// fusionner movbox
        Forest,// fusionner movbox
        CountrySide,
        Tower
    };




    private enum IndependantSource 
    {
        Ambiance,Clip,LifeBar,Musics

    }




    // ambiance dimension et biome

    public static AudioManagers instance;



    [SerializeField]
    private AudioMixer m_Mixer;


    [SerializeField]
    private BiomeStat m_Biomecurrent;


    [SerializeField]
    public DimensionScript.Dimension currentPlayerDimension;

    [SerializeField]
    public Animator m_Animator;
    
    [SerializeField]
    private AudioSource[] m_IndepandanteSource;


    [SerializeField]
    private List<SourceClipArray> ListAudioClipTranferts = new List<SourceClipArray>();

    [SerializeField]
    private List<BiomeAudioBaseArray> ListAudioAmbianceTranferts = new List<BiomeAudioBaseArray>();

    [SerializeField]
    private List<BiomeAudioBaseArray> ListAudioMusicsTranferts = new List<BiomeAudioBaseArray>();


    [SerializeField,Range(0,3)]
    private float SwapDimensionLerp;

    public enum Music
    {
        Menue1, Menue2, Pursuit1, Pursuit2,None
    }

    [SerializeField]
    private AudioClip[] m_Music;



    const string MIXER_NORMAL = "amb_irl";
    const string MIXER_SPECIAL = "amb_corrupted";
    const string MASTER_LEVEL = "MasterLevel";


    private bool SwapValueMixer;
    public bool GetMixerState;


    private List<SourceClipArray> GetClip(Transform child) 
    {
        List<SourceClipArray> array = new List<SourceClipArray>();

        for (int i = 0; i < child.childCount; i++)
        {
            array.Add(child.GetChild(i).GetComponent<SourceClipArray>());
        }

        return array;
    }

    private List<BiomeAudioBaseArray> GetAmbianceAndMusic(Transform child)
    {
        List<BiomeAudioBaseArray> array = new List<BiomeAudioBaseArray>();

        for (int i = 0; i < child.childCount; i++)
        {
            array.Add(child.GetChild(i).GetComponent<BiomeAudioBaseArray>());
        }

        return array;
    }


    public void LifeBarCheck(DimensionScript.Dimension dimensionPlayer) 
    {
        
        if(dimensionPlayer == DimensionScript.Dimension.Normal) 
        {
            m_IndepandanteSource[(int)IndependantSource.LifeBar].Stop();
        }
        else 
        {
            m_IndepandanteSource[(int)IndependantSource.LifeBar].Play();
        }
    }


    public void SwapVolume(int dimensionValue) 
    {



        if (m_IndepandanteSource[(int)DimensionScript.Dimension.Normal].volume > m_IndepandanteSource[(int)DimensionScript.Dimension.Special].volume) 
        {
            m_IndepandanteSource[(int)DimensionScript.Dimension.Normal].volume = 0;
            m_IndepandanteSource[(int)DimensionScript.Dimension.Special].volume = 1;

        }
        else 
        {
           
              m_IndepandanteSource[(int)DimensionScript.Dimension.Normal].volume = 1;
                
                

               m_IndepandanteSource[(int)DimensionScript.Dimension.Special].volume = 0;
        }

        
    }


    private void Awake()
    {
        m_IndepandanteSource = GetComponentsInChildren<AudioSource>();
       
        m_Animator = GetComponent<Animator>();
        if (this != instance)
        {
            instance = this;
        }

        // ListAudioClipTranferts.AddRange(transform.GetComponentsInChildren<SourceClipArray>());

       ListAudioClipTranferts = GetClip(transform.GetChild((int)IndependantSource.Clip));
       ListAudioAmbianceTranferts = GetAmbianceAndMusic(transform.GetChild((int)IndependantSource.Ambiance));


        m_IndepandanteSource[(int)IndependantSource.Musics].loop = true;
        UpdateAmbianceAndMusics();

    }


    public void UpdateAmbianceAndMusics()
    {

        foreach (var item in ListAudioAmbianceTranferts[(int)m_Biomecurrent].ClipArray)
        {
            if (item.Dimension == DimensionScript.Dimension.Normal )
            {
                m_IndepandanteSource[(int)item.Dimension].clip = item.sound.audioClip;

            } 

            if(item.Dimension == DimensionScript.Dimension.Special)
            {
                m_IndepandanteSource[(int)item.Dimension].clip = item.sound.audioClip;

            }
        }


        m_IndepandanteSource[(int)DimensionScript.Dimension.Normal].loop = true;
        m_IndepandanteSource[(int)DimensionScript.Dimension.Normal].Play();
        m_IndepandanteSource[(int)DimensionScript.Dimension.Special].loop = true;
        m_IndepandanteSource[(int)DimensionScript.Dimension.Special].Play();

    }




    public void PlayAudioAt(SourceFrom sourceFrom, BiomeStat Biome, AudioSource source)
    {
        int sourceFromInt = (int)sourceFrom;
        int BiomeFromInt = (int)Biome;
        


        List<AudioClipTranfert> audioclipList = new List<AudioClipTranfert>();

        foreach (var item in ListAudioClipTranferts[sourceFromInt].ClipArray)
        {
            if (item.ClipBiome == Biome)
            {
                audioclipList.Add(item);
            }
        }

        source.clip = audioclipList[UnityEngine.Random.Range(0, audioclipList.Count)].sound.audioClip;        


    }

    public void PlayMusic(Music music) 
    {
        if(music == Music.None) 
        {
            m_IndepandanteSource[(int)IndependantSource.Musics].Stop();
            m_IndepandanteSource[(int)IndependantSource.Musics].clip = null;
            return;
        }

        m_IndepandanteSource[(int)IndependantSource.Musics].clip = m_Music[(int)music];
    }






    public void UpdateAmbianceAndMusics(BiomeStat Biome)
    {
        this.m_Biomecurrent = Biome;
    }




}
