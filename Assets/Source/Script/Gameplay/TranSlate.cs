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


    protected float m_SwapDimensionTime;

    [SerializeField]
    protected float m_SwapDimensionTimeMax;





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
        CurrentObjectDimension= GetComponent<DimensionScipt>();

    }

    protected void Start()
    {
        
    }




    // need to change it 
    public void Translation() 
    {
        CurrentObjectDimension.OnClamping.RemoveAllListeners();

        DimensionScipt.Dimension currentDimension = CurrentObjectDimension.CurrentDimension;

        Vector3 swapVector = startPos;

        swapVector.z = -startPos.z;

        Debug.Log(swapVector);
     
        gameObject.transform.position = Vector3.Lerp(startPos, swapVector, m_SwapDimensionTime);




        if(m_SwapDimensionTime >= m_SwapDimensionTimeMax) 
        {
            m_SwapDimensionTime = 0;
            m_isTranslate = false;
            CurrentObjectDimension.SwapDimension();
            CurrentObjectDimension.OnClamping.AddListener(CurrentObjectDimension.ClampPositionPlayer);

        }

        m_SwapDimensionTime += Time.deltaTime;



    }



    // Update is called once per frame
    protected void Update()
    {
        if (m_isTranslate)
            Translation();
    }
    
}
