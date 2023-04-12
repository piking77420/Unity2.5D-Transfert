using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;





public class PlayerTranslate : TranSlate
{
    // Start is called before the first frame update

    [Space]


    [SerializeField]
    private PlayerGhostDetection m_PlayerGhost;

    [SerializeField]
    private MeshRenderer m_PlayerGraphics;

    public override void OnChangingDimension(InputAction.CallbackContext _context)
    {

        Debug.Log(m_PlayerGhost.IsCanPlayerTranslate());

        if (_context.canceled && m_PlayerGhost.IsCanPlayerTranslate())
        {
            m_Animator.SetTrigger("Translate");

            DimensionScript.Dimension current = CurrentObjectDimension.CurrentDimension;

            m_Animator.SetInteger("Dimension", (int)current);
            CurrentObjectDimension.OnClamping.RemoveListener(CurrentObjectDimension.ClampPositionPlayer);
        }

    }





    public override void Translation()
    {
        m_Rigidbody.velocity = Vector3.zero;
        m_PlayerGhost.GetComponent<MeshRenderer>().enabled = !m_PlayerGhost.GetComponent<Renderer>().enabled;

        m_PlayerGraphics.enabled = !m_PlayerGraphics.enabled;
        
    }


    private new void Awake()
    {   
        base.Awake();
        m_PlayerGraphics = GetComponentInChildren<MeshRenderer>();
    }
}
