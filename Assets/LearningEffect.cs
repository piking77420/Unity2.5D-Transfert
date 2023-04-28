using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LearningEffect : MonoBehaviour
{

    [SerializeField,Range(0,10)]
    private float LifeTime;

    [SerializeField]
    private VisualEffect m_VisualEffect;


    [Space,Header("Audio")]

    [SerializeField]
    private AudioSource m_AudioSource; 


    [SerializeField]
    private AudioClip m_AudioClip;

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_VisualEffect = GetComponentInChildren<VisualEffect>();
        m_AudioSource.clip = m_AudioClip;
        m_VisualEffect.enabled = false;
    }

    public IEnumerator SpawnEffect() 
    {
        m_VisualEffect.enabled = true;
        m_AudioSource.Play();
        yield return new WaitForSeconds(LifeTime);
        m_AudioSource.Stop();
        m_VisualEffect.enabled = false;

    }


}
