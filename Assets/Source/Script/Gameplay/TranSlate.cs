using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DimensionScipt;
using UnityEngine.InputSystem;
using Unity.Jobs;
using UnityEngine.Events;


[RequireComponent(typeof(DimensionScipt))]
public class TranSlate : MonoBehaviour
{
    // Start is called before the first frame update

    

    [SerializeField]
    protected DimensionScipt CurrentObjectDimension;



    [SerializeField]
    protected float m_SwapDimensionTime;


    protected float m_SwapDimensionTimer;
    protected float m_SwapDimensionTimerMax;





    [SerializeField]
    public bool m_isTranslate;



    public Vector3 startPos;


        public virtual void OnChangingDimension(InputAction.CallbackContext _context)
        {
            if (_context.performed && !m_isTranslate)
            {
                m_isTranslate = true;
                startPos = transform.position;
            }

         }





    protected void Awake()
    {
        CurrentObjectDimension = GetComponent<DimensionScipt>();
        m_SwapDimensionTimer = 1;
        m_SwapDimensionTimerMax = m_SwapDimensionTimer;
        m_SwapDimensionTimer = 0;

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
    public void Translation() 
    {


        PlayerGravityStatut(false);
        CurrentObjectDimension.OnClamping.RemoveAllListeners();
        DimensionScipt.Dimension currentDimension = CurrentObjectDimension.CurrentDimension;
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

    }



    // Update is called once per frame
    protected void Update()
    {
        if (m_isTranslate)
            Translation();
    }
    
}
