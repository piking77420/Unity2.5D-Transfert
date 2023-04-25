using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepDistance : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField,Range(0,2)]
    private float m_TouchRadius;

    [SerializeField]
    private AudioSource m_Source;

    [SerializeField]
    private AudioManagers.SourceFrom PlayerFootStep;
    

    private void Awake()
    {
        m_Source = GetComponent<AudioSource>();
    }
    

    public bool CastRayToGround()    
   {
        Collider[] sphere = Physics.OverlapSphere(this.transform.position, m_TouchRadius);

        
        foreach (var coll in sphere)
        {
            if(coll.gameObject.TryGetComponent<SoundEffect>(out SoundEffect effect))
            {
                //AudioManagers.instance.PlayAudioAt()
            }
        }
        return false;
   }
    


}
