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


    private void Awake()
    {
        m_VisualEffect = GetComponentInChildren<VisualEffect>();
        m_VisualEffect.enabled = false;
    }

    public IEnumerator SpawnEffect() 
    {
        m_VisualEffect.enabled = true;
        yield return new WaitForSeconds(LifeTime);
        m_VisualEffect.enabled = false;

    }


}
