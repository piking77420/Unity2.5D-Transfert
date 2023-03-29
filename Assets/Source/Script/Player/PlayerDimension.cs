using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class PlayerDimension : MonoBehaviour
{
    public enum Dimension 
    {
        Normal,Special
    }

    [SerializeField]
    public Dimension CurrentDimension;


    [SerializeField]
    public float DimensionSize;

   public UnityEvent OnClamping;



    private void Awake()
    {
        CurrentDimension = Dimension.Normal;
        OnClamping.AddListener(ClampPositionPlayer);
    }


    public void SwapDimension()
    {
        if (CurrentDimension == Dimension.Normal)
        {
            CurrentDimension = Dimension.Special;
        }
        else
        {
            CurrentDimension = Dimension.Normal;
        }
    }





    public void ClampPositionPlayer() 
    {
        Vector3 offsetVector = transform.position;

        


        if (CurrentDimension == Dimension.Normal) 
        {
            Debug.Log("here");

            if (transform.position.z < -DimensionSize) 
            {
                offsetVector.z = -DimensionSize ;
                transform.position = offsetVector;
            }else if (transform.position.z > 0) 
            {
                offsetVector.z = 0;
                transform.position = offsetVector;
            }
        }
        else 
        {
            if (transform.position.z > DimensionSize)
            {
                offsetVector.z = DimensionSize ;
                transform.position = offsetVector;
            }
            else if (transform.position.z < 0)
            {
                offsetVector.z = 0;
                transform.position = offsetVector;
            }
        }
    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnClamping.Invoke();
    }
}
