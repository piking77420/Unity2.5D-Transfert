using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetPursuit : MonoBehaviour
{
    // Start is called before the first frame update

    private enum OnCollisonBehaviours 
    {
        StartPursuit,EndPursuit
    }



    [SerializeField]
    private EnemyFollowPlayer followPlayer;

    [SerializeField]
    private OnCollisonBehaviours onCollisonBehaviours;
    [SerializeField]
    private AudioManagers.Music music;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerLearningSkill>(out PlayerLearningSkill player))
        {
            if (onCollisonBehaviours == OnCollisonBehaviours.StartPursuit)
            {

                followPlayer.playerTransform = followPlayer.transform;
                AudioManagers.instance.PlayMusic(music);
            }
            else 
            {
                followPlayer.playerTransform = null;
            }
                
        }
                
    }


}
