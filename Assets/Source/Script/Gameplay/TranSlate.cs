using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DimensionScript;
using UnityEngine.InputSystem;
using Unity.Jobs;
using UnityEngine.Events;
using Unity.VisualScripting;

[RequireComponent(typeof(DimensionScript))]
public class TranSlate : MonoBehaviour
{
    // Start is called before the first frame update

    

    [SerializeField]
    protected DimensionScript CurrentObjectDimension;



    [SerializeField]
    protected float m_SwapDimensionTime;


    protected float m_SwapDimensionTimer;
    protected float m_SwapDimensionTimerMax;





    private GameObject TranslatioEffect;
    [SerializeField]
    private Animator m_animator;



    public bool m_isTranslate;



    public Vector3 startPos;

    








    public virtual void OnChangingDimension(InputAction.CallbackContext _context)
    {

            if (_context.canceled && !m_isTranslate)
            {
                m_animator.SetTrigger("Translate");
                m_isTranslate = true;
                startPos = transform.position;
            

                TranslatioEffect.TryGetComponent<DistortionScriptEffect>(out var effect);
                effect.clockDirection = DistortionScriptEffect.ClockDirection.ClockWise;
                Instantiate(TranslatioEffect);
            }

    }





    protected void Awake()
    {
        m_animator = GetComponent<Animator>();
        CurrentObjectDimension= gameObject.GetComponent<DimensionScript>();
        m_SwapDimensionTimer = 1;
        m_SwapDimensionTimerMax = m_SwapDimensionTimer;
        m_SwapDimensionTimer = 0;

        TranslatioEffect = new GameObject();

        TranslatioEffect.AddComponent<DistortionScriptEffect>();    

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
    {

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


        /*
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
            PlayerGravityStatut(true);


        }
        */
    }



    // Update is called once per frame
    protected void Update()
    {
        if (m_isTranslate)
            Translation();
    }
    
}
