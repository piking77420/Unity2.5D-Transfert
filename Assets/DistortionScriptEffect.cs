using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DistortionScriptEffect : MonoBehaviour
{
    public enum ClockDirection 
    {
        ClockWise,
        Anticlockwise
    };



    [SerializeField]
    public ClockDirection clockDirection;

    [SerializeField]
    private Renderer m_Renderer;

    [SerializeField, Range(0.25f, 10)]
    public float lifetime;

    [SerializeField]
    private float m_DistortionAmout = 0.25f;





    [SerializeField]
    private float m_DistortionScale = 30f;

    [SerializeField]
    private float m_RotationAMOUT = 1f;

    [SerializeField]
    private float m_TriwlStrenght = 15.25f;







    [SerializeField]
    private float m_DistorAmoutCurrent;

    [SerializeField]
    private float m_DistorAmoutStartValue;
    [SerializeField]
    private float m_DistorAmoutEndValue;


    private void OnValidate()
    {

        if (clockDirection == ClockDirection.ClockWise)
        {
            m_DistorAmoutStartValue = m_DistortionAmout;
            m_DistorAmoutCurrent = m_DistorAmoutStartValue;
            m_DistorAmoutEndValue = 0f;
        }
        else
        {
            m_DistorAmoutStartValue = 0;
            m_DistorAmoutCurrent = m_DistorAmoutStartValue;
            m_DistorAmoutEndValue = m_DistortionAmout;
        }
    }




    public DistortionScriptEffect() 
    {
        clockDirection = ClockDirection.ClockWise;
        m_DistortionAmout = 0.25f;
        m_DistortionScale = 30f;
        m_RotationAMOUT = 1f;
        m_TriwlStrenght = 15.25f;
        m_DistorAmoutStartValue = m_DistortionAmout;
        m_DistorAmoutEndValue = 0;
    }

    public DistortionScriptEffect(ClockDirection clockDirection, float DistortionAmout, float lifetime) : base()
    {
        this.clockDirection = clockDirection;
        this.m_DistortionAmout = DistortionAmout;
        this.lifetime = lifetime;   
    }










    private void Awake()
    {
        m_Renderer = GetComponent<MeshRenderer>();

    }

    // Start is called before the first frame update
    void Start()
    {
    
        if(clockDirection == ClockDirection.ClockWise) 
        {
            m_DistorAmoutStartValue = m_DistortionAmout;
            m_DistorAmoutCurrent = m_DistorAmoutStartValue;   
            m_DistorAmoutEndValue = 0f;
        }
        else 
        {
            m_DistorAmoutStartValue = 0;
            m_DistorAmoutCurrent = m_DistorAmoutStartValue;
            m_DistorAmoutEndValue = m_DistortionAmout;
        }




    }

    // Update is called once per frame



    private void EffectLifeTimeAntiClocWise() 
    {
        if (m_DistorAmoutCurrent <= m_DistorAmoutEndValue - 0.01f)
        {
            m_DistorAmoutCurrent += (m_DistorAmoutEndValue / lifetime) * Time.deltaTime;
            m_Renderer.material.SetFloat("_DistortionAmout", m_DistorAmoutCurrent);


        }
        else
        {
          Destroy(gameObject);
        }
    }



    private void EffectLifeTimeClocWise() 
    {

        // add this condition cause if it's under 0 strange effect appeares
        if (m_DistorAmoutCurrent >= m_DistorAmoutEndValue + 0.01f)
        {
            m_DistorAmoutCurrent -= (m_DistorAmoutEndValue / lifetime) * Time.deltaTime;
            m_Renderer.material.SetFloat("_DistortionAmout", m_DistorAmoutCurrent);


        }
        else
        { 
            Destroy(gameObject);
        }
    }


    void Update()
    {
        m_Renderer.material.SetFloat("_DistortionScale", m_DistortionScale);
        m_Renderer.material.SetFloat("_RotationAmout", m_RotationAMOUT);


        m_Renderer.material.SetFloat("_TriwlStrenght", m_TriwlStrenght);

        if(clockDirection == ClockDirection.ClockWise) 
        {
            EffectLifeTimeClocWise();
        }
        else 
        {
            EffectLifeTimeAntiClocWise();
        }


    }
}
