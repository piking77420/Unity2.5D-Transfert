using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngineInternal;
using static Unity.VisualScripting.Member;
using static UnityEditor.Progress;
using static UnityEngine.InputSystem.InputAction;

public class BoxSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_AudioSource;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private Joint joint;

    [SerializeField, Range(0, 4)]
    private float m_Collradius;

    [SerializeField]
    private PlayerInput m_PlayerInput;

    private void Awake()
    {
        rb  = GetComponent<Rigidbody>();
        joint = GetComponent<Joint>();
        m_AudioSource= GetComponent<AudioSource>();
        m_PlayerInput = GetComponent<PlayerInput>();
    }

  
    public void OnMoveBoxStart(InputAction.CallbackContext _callbackContext) 
    {
      

        if(_callbackContext.started) 
        {
            PlaySound(AudioManagers.SourceFrom.MoveBox_Start);
            m_PlayerInput.SwitchCurrentActionMap("MoveBox");

        }


    }

    public void OnMoveBoxQuit(InputAction.CallbackContext _callbackContext)
    {
        if (_callbackContext.performed)
        {
            PlaySound(AudioManagers.SourceFrom.MoveBox_End);
        }

        if (_callbackContext.canceled)
        {
            m_AudioSource.Stop();
        }
    }


    private void PlaySound(AudioManagers.SourceFrom source) 
    {
        Collider[] coll = Physics.OverlapSphere(this.transform.GetChild(0).position, m_Collradius);

        foreach (var item in coll)
        {
            if (item.gameObject.TryGetComponent<SoundEffect>(out SoundEffect effect) && !m_AudioSource.isPlaying)
            {

                AudioManagers.instance.PlayAudioAt(source, effect.m_BiomeStat, m_AudioSource);
                m_AudioSource.PlayOneShot(m_AudioSource.clip);

            }
        }
            

        


    }


    void FixedUpdate()
    {
        if (joint.connectedBody == null)
        {
            m_PlayerInput.SwitchCurrentActionMap("GamePlay");
            return;
        }
      


    

        if (rb.velocity.magnitude > 4 ) 
        {
            if(!m_AudioSource.isPlaying)
            PlaySound(AudioManagers.SourceFrom.MoveBox_Move);
            return;
        }
    }
}
