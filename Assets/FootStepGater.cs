using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepGater : MonoBehaviour
{


    FootStepDistance[] PlayerFoot;


    



    private void Awake()
    {
       PlayerFoot = GetComponentsInChildren<FootStepDistance>();
    }


    private void PlayerFootStep()
    {

      //AudioManagers.instance.PlayAudioAt("FootStep_run_countryside");


    }

}
