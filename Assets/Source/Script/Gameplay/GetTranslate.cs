using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTranslate : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private TranSlate m_Translate;




    private void Awake()
    {
        m_Translate= GetComponentInChildren<TranSlate>();
    }


    private void OnStartTranlation()
    {
        m_Translate.StartTranslation();
    }

        
    private void OnEndTranlation() 
    {
        m_Translate.EndTranslation();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
