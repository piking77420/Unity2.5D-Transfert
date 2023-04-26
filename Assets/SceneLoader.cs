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


    [SerializeField]
    private float m_SecondsToLoad;

    IEnumerator LoadScene() 
    {
        m_Animator.SetTrigger("FadeOutChanageScene");
        yield return new WaitForSeconds(m_SecondsToLoad);
        SceneManager.LoadScene(m_SceneToLoad);

    }



    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") 
        {
            other.GetComponentInParent<PlayerStatus>().IsInvicible = true;
            m_Animator.SetTrigger("FadeOutChanageScene");
            StartCoroutine(LoadScene());
        }
    }



}
