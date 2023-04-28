using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SteleLearningScipt : MonoBehaviour
{
    // Start is called before the first frame update

   


    [SerializeField]
    private PlayerLearningSkill.PlayerSkill m_SkillToLearn;



    [SerializeField]
    private VisualEffect m_VisualEffect;

    [SerializeField, Range(0, 5)]
    private float m_LearningRadius;


    [SerializeField]
    private bool m_ShowGizmo;

    private void OnDrawGizmos()
    {
        if (m_ShowGizmo)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(this.transform.position, m_LearningRadius);

        }
    }




    void Start()
    {
        m_VisualEffect =  GetComponentInChildren<VisualEffect>();    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider[] coll = Physics.OverlapSphere(this.transform.position, m_LearningRadius);


        foreach (var item in coll)
        {
            if (item.TryGetComponent<PlayerLearningSkill>(out PlayerLearningSkill PlayerLearning))
            {
                PlayerLearning.LearnSkill(m_SkillToLearn);
                m_VisualEffect.enabled = false;
            }
        }
    }
}
