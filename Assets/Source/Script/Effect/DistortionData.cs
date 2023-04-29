using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DistortionData : EffectData
{



    [SerializeField]
    public DistortionScriptEffect.ClockDirection clockDirection;



    public float m_DistortionAmout = 0.25f;


    public float m_DistortionScale = 30f;


    public float m_RotationAMOUT = 1f;


    public float m_TriwlStrenght = 15.25f;


    public float m_DistorAmoutCurrent;


    public float m_DistorAmoutStartValue;

    public float m_DistorAmoutEndValue;


}
