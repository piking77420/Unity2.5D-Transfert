using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTranslate : TranSlate
{
    // Start is called before the first frame update



    public override void OnChangingDimension(InputAction.CallbackContext _context)
    {

            


        if (_context.canceled && !m_isTranslate)
        {
            m_isTranslate = true;
            startPos = transform.position;
        }

    }


     private new void Awake()
    {
        base.Awake();
        
    }
    public virtual void Translation()
    {

    }



        new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
