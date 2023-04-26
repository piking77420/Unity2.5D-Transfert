using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioManagers;


[RequireComponent(typeof(AudioSource))]
public class FootStepDistance : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField,Range(0,2)]
    private float m_TouchRadius;

    [SerializeField]
    private AudioSource m_Source;


 




    [SerializeField]
    private bool m_ShowGizmo;




    private void OnDrawGizmos()
    {
        if (m_ShowGizmo) 
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(this.transform.position, m_TouchRadius);
        }
    }

    private void Awake()
    {
        m_Source = GetComponent<AudioSource>();
    }
    

    public void PlayFootStep(AudioManagers.SourceFrom sourcefrom) 
    {
        AudioManagers.BiomeStat currentBiom = GetBiomeCollider();

        AudioManagers.instance.PlayAudioAt(sourcefrom, currentBiom, m_Source);

        m_Source.PlayOneShot(m_Source.clip);
    }




    public AudioManagers.BiomeStat GetBiomeCollider()    
   {
       
            Collider[] sphere = Physics.OverlapSphere(this.transform.position, m_TouchRadius);


            foreach (var coll in sphere)
            {
                Debug.Log(coll.gameObject.name);

                if (coll.gameObject.TryGetComponent<SoundEffect>(out SoundEffect effect))
                {
                 return effect.m_BiomeStat;
                }
            }

        return BiomeStat.Village;

      
   }
    


}
