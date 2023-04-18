using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class DimensionScript : MonoBehaviour
{
    public enum Dimension 
    {
        Normal,Special
    }

    [SerializeField]
    public Dimension CurrentDimension;


    [SerializeField]
    public  static float DimensionSize = 5;

   public UnityEvent OnClamping;



    private void Awake()
    {
        CurrentDimension = Dimension.Normal;
        OnClamping.AddListener(ClampPositionPlayer);
    }



    

    public void SwapDimension()
    {


        if(gameObject.transform.childCount != 0) 
        {
           Vector3 Newpos = this.gameObject.transform.GetChild(0).position;

            if (CurrentDimension == Dimension.Normal)
            {
                CurrentDimension = Dimension.Special;


                Newpos.z = (DimensionSize / 2f);
            }
            else
            {
                CurrentDimension = Dimension.Normal;
                Newpos.z = -(DimensionSize / 2f);
            }
            this.gameObject.transform.GetChild(0).position = Newpos;
        }
        else 
        {
            Vector3 Newpos = this.gameObject.transform.position;

            if (CurrentDimension == Dimension.Normal)
            {
                CurrentDimension = Dimension.Special;


                Newpos.z = (DimensionSize / 2f);
            }
            else
            {
                CurrentDimension = Dimension.Normal;
                Newpos.z = -(DimensionSize / 2f);
            }
            this.gameObject.transform.position = Newpos;
        }



    }



 

    public void ClampPositionPlayer() 
    {
        Vector3 offsetVector = transform.position;

        
        /*

        if (CurrentDimension == Dimension.Normal) 
        {

            if (transform.position.z < -DimensionSize) 
            {
                offsetVector.z = -offsetVector.z;
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
                offsetVector.z = -offsetVector.z; ;
                transform.position = offsetVector;
            }
            else if (transform.position.z < 0)
            {
                offsetVector.z = 0;
                transform.position = offsetVector;
            }
        }

        */

        if(CurrentDimension == Dimension.Normal) 
        {
            transform.position = offsetVector;
        }
    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      //OnClamping.Invoke();
       
    }
}
