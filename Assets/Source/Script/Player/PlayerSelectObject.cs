using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelectObject : MonoBehaviour
{
    // Start is called before the first frame update


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

    }


    void Start()
    {
        FindAllSelectableObject();
    }

    // Update is called once per frame
  
}
