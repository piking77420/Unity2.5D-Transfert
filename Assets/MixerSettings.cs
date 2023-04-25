using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerSettings : MonoBehaviour
{
    [SerializeField]
    private AudioMixer m_Mixer;


    [SerializeField]
    private Slider m_Slider_Master;

    [SerializeField]
    private Slider m_Slider_Musics;
    [SerializeField]
    private Slider m_Slider_SFX;
    [SerializeField]
    private Slider m_Slider_Voice;


    const string MIXER_MASTER = "Master";
    const string MIXER_MUSICS = "Musics";
    const string MIXER_SFX = "SFX";
    const string MIXER_VOICE = "Voice";


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
