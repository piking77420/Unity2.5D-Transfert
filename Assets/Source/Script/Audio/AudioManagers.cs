using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManagers : MonoBehaviour
{
    // Start is called before the first frame update
    public enum BiomeStat
    {
        Interior,
        Interior2,
        Village,// fusionner movbox
        Forest,// fusionner movbox
        CountrySide,

    };

    public enum SourceFrom 
    {
        FootPlayer,
        Box,
        Slime,
        TheEnemy,
    };



    // ambiance dimension et biome

    public static AudioManagers instance;

    public GameObject m_prefabs;

    [Serialize]
    private MusicClass[] m_Musics;



    [SerializeField]
    private AudioSource m_CurrentSong;


    [SerializeField]
    private List<SourceClipArray> ListAudioClipTranferts = new List<SourceClipArray>();




    private void Awake()
    {
        m_CurrentSong = GetComponent<AudioSource>();
        if (m_CurrentSong == null) 
        {
            m_CurrentSong.AddComponent<AudioSource>();  
        }

        if(this != instance) 
        {
            instance = this;
        }


        
        int nbrOfArray = transform.GetChild(0).childCount;


        for (int i = 0; i < nbrOfArray; i++)
        {


            
                ListAudioClipTranferts.Add(transform.GetChild(0).GetChild(i).GetComponent<SourceClipArray>());

            
        }
     
    }

   
    public void PlayMusic(string name) 
    {
        
    }

  
    public void PlayAudioAt(SourceFrom sourceFrom , BiomeStat Biome , AudioSource source) 
    {

        for (int i = 0; i < ListAudioClipTranferts.Count; i++)
        {
            if (ListAudioClipTranferts[i].SourceType == sourceFrom)
            for (int k = 0; k < ListAudioClipTranferts[i].ClipArray.Length; k++)
            {
                    if (ListAudioClipTranferts[i].ClipArray[k].ClipBiome == Biome) 
                    {
                        source.clip = ListAudioClipTranferts[i].ClipArray[k].AuduioClip;
                    }

            }
        }
    
        //Instantiate(m_prefabs,transform,true);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
