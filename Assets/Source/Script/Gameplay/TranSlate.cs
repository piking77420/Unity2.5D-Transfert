using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DimensionScript;
using UnityEngine.InputSystem;
using Unity.Jobs;
using UnityEngine.Events;
using Unity.VisualScripting;
using static UnityEngine.Rendering.DebugUI;
using System;

[RequireComponent(typeof(DimensionScript))]
public class TranSlate : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    protected EffectSpwaner m_EffectSpwaner;

    [SerializeField]
    protected DimensionScript CurrentObjectDimension;



    [SerializeField]
    protected float m_SwapDimensionTime;


    protected float m_SwapDimensionTimer;
    protected float m_SwapDimensionTimerMax;




    [SerializeField]
    private Animator m_animator;



    public bool m_isTranslate;



    public Vector3 startPos;









    private bool CheckIfIsPlayer(out PlayerMovement movementPlayer , out PlayerJump playerJump) 
    {
        if(TryGetComponent<PlayerJump>(out playerJump) && TryGetComponent<PlayerMovement>(out movementPlayer) )
        {

            return true;
        }
        else 
        {
            movementPlayer = null;
            playerJump = null;
            return false;
        }
       

    }



    IEnumerator PlayerBecommingGhosted(float seconds) 
    {   
        gameObject.TryGetComponent<Renderer>(out Renderer renderer);
        gameObject.TryGetComponent<Collider>(out Collider collider);

        bool ifIsPlayer = CheckIfIsPlayer(out PlayerMovement movementPlayer, out PlayerJump jump);





       if (ifIsPlayer) 
       {
            movementPlayer.enabled = false;
            jump.enabled = false;
       }

        collider.enabled = false;
        renderer.enabled = false;

        yield return new WaitForSeconds(seconds);

        collider.enabled = true;
        if (ifIsPlayer)
        {
            movementPlayer.enabled = true;
            jump.enabled = true;
        }
        renderer.enabled = true;
    }











    public virtual void OnChangingDimension(InputAction.CallbackContext _context)
    {
                


            if (_context.canceled && !m_isTranslate)
            {
                m_isTranslate = true;
                startPos = transform.position;
                
            }

    }





    protected void Awake()
    {
        m_animator = GetComponent<Animator>();
        CurrentObjectDimension= gameObject.GetComponent<DimensionScript>();
        m_SwapDimensionTimer = 1;
        m_SwapDimensionTimerMax = m_SwapDimensionTimer;
        m_SwapDimensionTimer = 0;
        m_EffectSpwaner = GetComponent<EffectSpwaner>();

        /*
        TranslatioEffect = new GameObject();

        TranslatioEffect.AddComponent<DistortionScriptEffect>();    */

    }

    protected void PlayerGravityStatut(bool value)
    {
        if(gameObject.TryGetComponent<PlayerJump>(out var playerJump)) 
        {
            playerJump.isGravityApplie = value;
        }

    }

    //void private
    protected void Start()
    {
      
    }

    // need to change it 
    public virtual void  Translation() 
    {/*

        CurrentObjectDimension.OnClamping.RemoveAllListeners();
        DimensionScript.Dimension currentDimension = CurrentObjectDimension.CurrentDimension;
        m_SwapDimensionTimer += Time.deltaTime;


        if (m_SwapDimensionTimer >= m_SwapDimensionTimerMax)
        {
            m_SwapDimensionTimer = 0;
            m_isTranslate = false;
            CurrentObjectDimension.SwapDimension();
            CurrentObjectDimension.OnClamping.AddListener(CurrentObjectDimension.ClampPositionPlayer);

 
        }

        */  
        
        PlayerGravityStatut(false);
        CurrentObjectDimension.OnClamping.RemoveAllListeners();
        DimensionScript.Dimension currentDimension = CurrentObjectDimension.CurrentDimension;
        Vector3 swapVector = startPos;




        swapVector.z = -startPos.z;

        gameObject.transform.position = Vector3.Lerp(startPos, swapVector, m_SwapDimensionTimer);

        m_SwapDimensionTimer += Time.deltaTime  ;

        if (m_SwapDimensionTimer >= m_SwapDimensionTimerMax)
        {
            m_SwapDimensionTimer = 0;
            m_isTranslate = false;
            CurrentObjectDimension.SwapDimension();
            CurrentObjectDimension.OnClamping.AddListener(CurrentObjectDimension.ClampPositionPlayer);

            m_EffectSpwaner.SpawnEffect();
            PlayerGravityStatut(true);


        }
        
    }



    // Update is called once per frame
    protected void Update()
    {
        if (m_isTranslate)
            Translation();
    }
    
}
