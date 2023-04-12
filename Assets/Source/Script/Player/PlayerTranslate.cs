using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;





public class PlayerTranslate : TranSlate
{
    // Start is called before the first frame update

    [Space]

    [SerializeField]
    private Animator m_Animator;


    [SerializeField]
    private PlayerGhostDetection m_PlayerGhost;

 



    public new void BecommingGhosted() 
    {
        gameObject.TryGetComponent<Renderer>(out Renderer renderer);
        gameObject.TryGetComponent<Collider>(out Collider collider);
        gameObject.TryGetComponent<PlayerJump>(out PlayerJump playerJump);
        gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement movementPlayer);

        movementPlayer.enabled = !movementPlayer.enabled;
        playerJump.enabled = !playerJump.enabled;
        collider.enabled = !collider.enabled;
        renderer.enabled = !collider.enabled;

            


    }



    public override void OnChangingDimension(InputAction.CallbackContext _context)
    {



        if (_context.canceled && !m_isTranslate)
        {
            m_isTranslate = true;
            startPos = transform.position;
            m_Animator.SetTrigger("Translate");

            DimensionScript.Dimension current = CurrentObjectDimension.CurrentDimension;

            m_Animator.SetInteger("Dimension", (int)current);
            CurrentObjectDimension.OnClamping.RemoveListener(CurrentObjectDimension.ClampPositionPlayer);
        }
        m_isTranslate = false;
        CurrentObjectDimension.OnClamping.AddListener(CurrentObjectDimension.ClampPositionPlayer);

    }






    private new void Awake()
    {   
        base.Awake();
        m_Animator = GetComponentInParent<Animator>();
        
    }
 




        

    new void Start()
    {
        base.Start();

    }

    // Update is called once per frame



    

    new void Update()
    {
        if (m_PlayerGhost.IsCanPlayerTranslate()) 
        {
            base.Update();
        }

        
    }
}
