using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class SelectAudioSource : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] private AudioSource m_AudioSource;


    [Header("List of all audio for GameObject"),Space]
    
    [SerializeField]    
    public AudioClip playerJump;

    [SerializeField]
    public AudioClip playerDied;

    [SerializeField]
    public AudioClip playerTranslate;

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip source) 
    {
        m_AudioSource.clip = source;
        m_AudioSource.Play();
    }


  
}
