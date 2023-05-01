using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{


    [SerializeField]
    private string m_SceneToLoad;

    [SerializeField]
    private FadeSystem m_FadeSystem;


    [SerializeField]
    private float m_SecondsToLoad;

    private void Awake()
    {
        m_FadeSystem = FindObjectOfType<FadeSystem>();
    }



    IEnumerator LoadScene() 
    {
        m_FadeSystem.OnEndLevel();
        yield return new WaitForSeconds(m_SecondsToLoad);
        SceneManager.LoadScene(m_SceneToLoad);

    }



    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") 
        {
            other.GetComponentInParent<PlayerStatus>().IsInvicible = true;
            StartCoroutine(LoadScene());
        }
    }



}
