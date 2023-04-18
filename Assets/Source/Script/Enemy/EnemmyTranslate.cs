using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemmyTranslate : TranSlate
{
    // Start is called before the first frame update

    [SerializeField]
    private EnemyPatrol m_EnemyPatrol;

    public override void OnChangingDimension(InputAction.CallbackContext _context)
    {



        if (_context.canceled && isTranslate)
        {
            m_Animator.SetTrigger("Translate");

            DimensionScript.Dimension current = CurrentObjectDimension.CurrentDimension;

            m_Animator.SetInteger("Dimension", (int)current);
            isTranslate = false;
        }

    }



    new void Awake() 
    {
        base.Awake();

        m_EnemyPatrol= GetComponent<EnemyPatrol>();
    }


    new void  Start()
    {
        base.Start();   
    }

    public override void StartTranslation()
    {

        base.StartTranslation();
        m_EnemyPatrol.enabled= false;

    }

    public override void EndTranslation()
    {

        base.EndTranslation();
        m_EnemyPatrol.enabled = true;

        m_EnemyPatrol.OnChangingDimension();
        

    }

    // Update is called once per frame

}
