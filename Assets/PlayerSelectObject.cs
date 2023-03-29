using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelectObject : MonoBehaviour
{
    // Start is called before the first frame update


   private Camera m_mainCam;
    [SerializeField]
    private List<SelectableObject> m_SelectableObejct;


  




    

    public void FindAllSelectableObject() 
    {

        List<GameObject> rootObjects = new List<GameObject>();
        UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);

        foreach (var objet in rootObjects)
        {


            if (objet.TryGetComponent<SelectableObject>(out SelectableObject current) && !m_SelectableObejct.Contains(current))
            {
                m_SelectableObejct.Add(objet.GetComponent<SelectableObject>());
            }
        }
    }




    private void Awake()
    {
        m_mainCam = Camera.main;
    }


    void Start()
    {
        FindAllSelectableObject();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 ScreenMin = new Vector3(0, 0, 0);
        Vector3 ScreenMax = new Vector3(m_mainCam.scaledPixelWidth, m_mainCam.scaledPixelHeight, 0);

        ScreenMax = m_mainCam.ScreenToViewportPoint(ScreenMax);
        ScreenMin = m_mainCam.ScreenToViewportPoint(ScreenMin);


    }
}
