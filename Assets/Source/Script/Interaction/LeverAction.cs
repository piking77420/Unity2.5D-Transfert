using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LeverAction : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private LevierIntercation m_LevierIntercation;

    [SerializeField]
    private Animator m_Animator;


    [SerializeField, Range(0f, 1f)]
    private float m_LeverValueStatue;


    [SerializeField, Tooltip("EVENT PLAY EACH FRAME")]
    public UnityEvent<float> OnAccomplish;

    [SerializeField, Tooltip("EVENT PLAY EACH FRAME")]
    public UnityEvent<float> OnNonAccomplish;

    public UnityEvent OnQuitLever;

    [Header("Audio")]
    [SerializeField]
    private AudioSource m_AudioSource;
    [SerializeField]
    private AudioClip[] m_AudioClip;

    [Space(2)]

    [SerializeField, Range(0,1)]
    private float OpenDoorStreght;

    private float LeverPlayerStrenght;


    [SerializeField, Range(0,1)]
    private float DecreaseStreght;



    [SerializeField]
    public bool IsAcomplish;
    [SerializeField]
    public bool PlayerInAction;

    [SerializeField]
    public bool IsObstruct;



    [SerializeField]
    public float m_LeverReadValue;
    public void OnQuit(InputAction.CallbackContext _callbackContext)
    {
        if (_callbackContext.performed)
        {
            PlayerInAction = false;
            m_AudioSource.Stop();
            OnQuitLever.Invoke();

        }

    }


    private void PlaySound() 
    {



        if (!m_AudioSource.isPlaying)
        {
            m_AudioSource.PlayOneShot(m_AudioClip[Random.Range(0,1)]);
        }
    }


    public void OnleverAction(InputAction.CallbackContext _callbackContext)
    {



        switch (_callbackContext.phase)
        {
            case InputActionPhase.Started:
                PlayerInAction = true;
                IsAcomplish = false;
                
                break;
            case InputActionPhase.Performed:

                float readValue = _callbackContext.ReadValue<float>();
                if (readValue <= 0f)
                {
                    m_LeverReadValue = -1f;
                }
                else if (readValue >= 0f)
                {
                    m_LeverReadValue = 1f;
                }
                else 
                {
                    m_LeverReadValue = 0;
                    m_AudioSource.Stop();

                }
                PlayerInAction = true;
                break;
            case InputActionPhase.Canceled:
                m_LeverReadValue = 0;
                PlayerInAction = false;

                break;

        }
    }







    private void Awake()
    {
        m_LevierIntercation = GetComponent<LevierIntercation>();
        m_Animator = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>();
        }




    private void OpenDoor()
    {
        // Had This to avoid when doing left right realy fast that opening the door to quciky
        if (PlayerInAction)
        {

            PlaySound();
            m_LeverValueStatue += Time.deltaTime  * m_LeverReadValue * OpenDoorStreght;
        }

        if (m_LeverValueStatue > 1f)
        {
            IsAcomplish = true;
        }


        m_LeverValueStatue = Mathf.Clamp01(m_LeverValueStatue);


    }


    private void CloseDoor()
    {

        if (IsObstruct)
        {

            return;
        }
        


        if (!PlayerInAction && !IsAcomplish && m_LeverValueStatue > 0)
        {
            m_LeverValueStatue -= Time.deltaTime * DecreaseStreght;
            OnNonAccomplish.Invoke(m_LeverValueStatue);

        }
    }

    private void CheckStatue()
    {
        if (m_LeverValueStatue >= 1f)
        {
            IsAcomplish = true;
            return;
        }

        if (m_LeverValueStatue <= 1f && PlayerInAction)
        {
            OnAccomplish?.Invoke(m_LeverValueStatue);
        }
        

        
    }


    // Update is called once per frame
    private void Update()
    {
        CheckStatue();

        OpenDoor();
        CloseDoor();

        m_Animator.SetFloat("Status", m_LeverValueStatue);
    }


}
