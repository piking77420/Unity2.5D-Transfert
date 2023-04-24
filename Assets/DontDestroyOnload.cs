using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnload : MonoBehaviour
{
    // Start is called before the first frame update
    public Object[] DontDestoyOnLoadArray;



    private void Awake()
    {
        foreach (var obj in DontDestoyOnLoadArray)
        {

           

            DontDestroyOnLoad(obj);
        }
    }


}
