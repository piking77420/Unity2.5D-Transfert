using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEditor.Search;
using UnityEngine;

public class PlayerLearningSkill : MonoBehaviour
{
    private enum PlayerSkill 
    {
        Translate , DragObject , ThroWEnemy,

    }
    [SerializeField]
    private PlayerSkill m_SkillToLearn;

    [SerializeField,Range(0,5)]
    private float m_LearningRadius;


    [SerializeField]
    private bool m_ShowGizmo;



    [SerializeField]
    private GameObject m_LearnignEffect;

    private void OnDrawGizmos()
    {
        if (m_ShowGizmo) 
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(this.transform.position, m_LearningRadius);

        }
    }

    private void LearnSkill(PlayerSkill skillToLearn,Collider coll) 
    {

        if(skillToLearn == PlayerSkill.Translate) 
        {
            if (coll.gameObject.TryGetComponent<PlayerTranslate>(out PlayerTranslate playerTranslate)) 
            {
                playerTranslate.enabled = true;
            }
        }
        else if (skillToLearn == PlayerSkill.DragObject) 
        {
            if (coll.gameObject.TryGetComponent<PlayerDragObject>(out PlayerDragObject playerDragObject))
            {
                playerDragObject.enabled = true;
            }
        }

        else if (skillToLearn == PlayerSkill.ThroWEnemy)
        {
            if (coll.gameObject.TryGetComponent<PlayerThrowEnemy>(out PlayerThrowEnemy playerThrowEnemy))
            {
                playerThrowEnemy.enabled = true;
            }
        }

     





    }




    private void FixedUpdate()
    {
        Collider[] coll = Physics.OverlapSphere(this.transform.position, m_LearningRadius);


        foreach (var item in coll)
        {
            if(item.TryGetComponent<PlayerGraphicUpdate>(out PlayerGraphicUpdate graphicUpdate)) 
            {
                LearnSkill(m_SkillToLearn, item);
                Instantiate(m_LearnignEffect, graphicUpdate.transform);
                
            }
        }
    }
}
