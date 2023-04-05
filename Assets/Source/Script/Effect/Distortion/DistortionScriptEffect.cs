using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.Mathematics;
using UnityEngine;




public class DistortionScriptEffect : Effect
{
    public enum ClockDirection
    {
        ClockWise,
        Anticlockwise
    };



    [SerializeField]
    public DistortionData m_data;







    protected new void Awake()
    {
        base.Awake();
     
    }

    // Start is called before the first frame update
    void Start()
    {
    
        if(m_data.clockDirection == ClockDirection.ClockWise) 
        {
            m_data.m_DistorAmoutStartValue = m_data.m_DistortionAmout;
            m_data.m_DistorAmoutCurrent = m_data.m_DistorAmoutStartValue;
            m_data.m_DistorAmoutEndValue = 0f;
        }
        else 
        {
            m_data.m_DistorAmoutStartValue = 0;
            m_data.m_DistorAmoutCurrent = m_data.m_DistorAmoutStartValue;
            m_data.m_DistorAmoutEndValue = m_data.m_DistortionAmout;
        }




    }

    // Update is called once per frame



    private void EffectLifeTimeAntiClocWise() 
    {
        if (m_data.m_DistorAmoutCurrent <= m_data.m_DistorAmoutEndValue - 0.01f)
        {
            m_data.m_DistorAmoutCurrent += (m_data.m_DistorAmoutEndValue / m_data.lifeTime) * Time.deltaTime;
            m_Renderer.material.SetFloat("_DistortionAmout", m_data.m_DistorAmoutCurrent);


        }
        else
        {
          Destroy(gameObject);
        }
    }



    private void EffectLifeTimeClocWise() 
    {

        // add this condition cause if it's under 0 strange effect appeares
        if (m_data.m_DistorAmoutCurrent >= m_data.m_DistorAmoutEndValue + 0.01f)
        {
            m_data.m_DistorAmoutCurrent -= (m_data.m_DistorAmoutStartValue / m_data.lifeTime) * Time.deltaTime;
            m_Renderer.material.SetFloat("_DistortionAmout", m_data.m_DistorAmoutCurrent);

        }
        else
        { 
            Destroy(gameObject);
        }
    }


    
   protected new void Update()
    {

        base.Awake();

        m_Renderer.material.SetFloat("_DistortionScale", m_data.m_DistortionScale);
        m_Renderer.material.SetFloat("_RotationAmout", m_data.m_RotationAMOUT);


        m_Renderer.material.SetFloat("_TriwlStrenght", m_data.m_TriwlStrenght);

        if(m_data.clockDirection == ClockDirection.ClockWise) 
        {
            EffectLifeTimeClocWise();   
        }
        else 
        {
            EffectLifeTimeAntiClocWise();
        }


    }
}
