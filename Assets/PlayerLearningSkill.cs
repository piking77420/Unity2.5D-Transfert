using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEditor.Search;
using UnityEngine;

public class PlayerLearningSkill : MonoBehaviour
{
    public enum PlayerSkill : int
    {
        Translate, DragObject, ThroWEnemy,

    }

    private LearningEffect LearnignEffect;

    private PlayerTranslate m_PlayerTranslate;

    private PlayerThrowEnemy m_PlayerThrowEnemy;

    private PlayerDragObject m_PlayerDragObject;

    private void Awake()
    {
        m_PlayerTranslate = GetComponentInParent<PlayerTranslate>();
        m_PlayerThrowEnemy = GetComponentInParent<PlayerThrowEnemy>();
        m_PlayerDragObject = GetComponentInParent<PlayerDragObject>();
        LearnignEffect = GetComponentInChildren<LearningEffect>();

    }


    private void SpawnEffect() 
    {
        StartCoroutine(LearnignEffect.SpawnEffect());
    }

    public void LearnSkill(PlayerSkill skillToLearn) 
    {

        if(skillToLearn == PlayerSkill.Translate) 
        {
            SpawnEffect();
            m_PlayerTranslate.ISLearned = true;
            
        }
        else if (skillToLearn == PlayerSkill.DragObject) 
        {
            SpawnEffect();
            m_PlayerThrowEnemy.IsLearned = true;
        }
        else if (skillToLearn == PlayerSkill.ThroWEnemy)
        {
            SpawnEffect();
            m_PlayerDragObject.IsLearned = true;
        }






        Destroy(this.gameObject.GetComponent<PlayerLearningSkill>());  
    }




    private void FixedUpdate()
    {
    
    }
}
