using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioSound 
{

    public AudioClip audioClip;    

    [Range(0,2)]
    public float volume;

    [Range(0, 2)]
    public float pitch;

    [Range(0, 2)]
    public float roll;

    [Range(0, 2)]
    public float duration;
}
