using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagers : MonoBehaviour
{
    // Start is called before the first frame update

    private AudioManagers instance;

    public GameObject m_prefabs;

    [SerializeField]
    private AudioSound[] m_AudioSounds;

    private void Awake()
    {
        if(this != instance) 
        {
            Debug.LogWarning("There is more than one AudioManagers");
            return;
        }


    }

    public void PlayAudioSound(string AudioName) 
    {
        foreach (var item in m_AudioSounds)
        {
            if(item.audioName == AudioName) 
            {
            }
        }
    }

    public void PlayAudioAt(string AudioName,Transform transform) 
    {
        



        Instantiate(m_prefabs,transform,true);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
