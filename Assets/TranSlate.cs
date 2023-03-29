using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerDimension;
using UnityEngine.InputSystem;
using Unity.Jobs;
using UnityEngine.Events;

public class TranSlate : MonoBehaviour
{
    // Start is called before the first frame update

    

    [SerializeField]
    private PlayerDimension m_PlayerDimension;


    private float m_SwapDimensionTime;

    [SerializeField]
    private float m_SwapDimensionTimeMax;





    [SerializeField]
    private bool m_PlayerTranslate;



    private Vector3 startPos;



   

    public void OnChangingDimension(InputAction.CallbackContext _context)
    {
        if(_context.performed)
        m_PlayerTranslate = true;

        startPos = transform.position;
    }





    private void Awake()
    {
        m_PlayerDimension= GetComponent<PlayerDimension>();

    }

    void Start()
    {
        
    }




    private void Translation() 
    {
        m_PlayerDimension.OnClamping.RemoveAllListeners();

        float dimenSionSize = m_PlayerDimension.DimensionSize;
        PlayerDimension.Dimension currentDimension = m_PlayerDimension.CurrentDimension;

        Vector3 swapVector = Vector3.zero;
        swapVector.z = dimenSionSize;

        if (currentDimension == Dimension.Normal)
        {
            Vector3 endPos = startPos + swapVector;
            gameObject.transform.position = Vector3.Lerp(startPos, endPos, m_SwapDimensionTime);


        }
        else
        {
            Vector3 endPos = startPos - swapVector;
            gameObject.transform.position = Vector3.Lerp(startPos, endPos, m_SwapDimensionTime);

        }




        if(m_SwapDimensionTime >= m_SwapDimensionTimeMax) 
        {
            m_SwapDimensionTime = 0;
            m_PlayerTranslate = false;
            m_PlayerDimension.SwapDimension();
            m_PlayerDimension.OnClamping.AddListener(m_PlayerDimension.ClampPositionPlayer);

        }

        m_SwapDimensionTime += Time.deltaTime;



    }



    // Update is called once per frame
    void Update()
    {
        if (m_PlayerTranslate)
            Translation();
    }
    
}
