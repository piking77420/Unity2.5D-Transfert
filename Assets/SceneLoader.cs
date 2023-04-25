using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{


    [SerializeField]
    private string m_SceneToLoad;

    [SerializeField]
    private Animator m_Animator;




    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") 
        {
            m_Animator.SetTrigger("FadeOutChanageScene");
            SceneManager.LoadScene(m_SceneToLoad);
        }
    }



}
