using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerStatus : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Dependencies")]

    [SerializeField]
    private PlayerInput m_PlayerInput;

    [SerializeField]
    private Animator m_Animator;

    [Header("Value")]

    [SerializeField]
    public bool IsDead;

    [SerializeField]
    public Vector3 PlayerCurrentCheckpoint;


    [HideInInspector,SerializeField]
    public UnityEvent OnPlayerDeath;



    private string OldActionMapBuffer;

    [SerializeField]
    private float TimerFadeDeath;


    public int currentCheckpointIndex;


    IEnumerator DeathPlayer()
    {

        m_Animator.SetBool("FadeIn",IsDead);
        m_PlayerInput.SwitchCurrentActionMap("None");
        yield return new WaitForSeconds(TimerFadeDeath/2f);
        transform.position = new Vector3(PlayerCurrentCheckpoint.x, PlayerCurrentCheckpoint.y, 0);
        yield return new WaitForSeconds(TimerFadeDeath/2f);
        m_PlayerInput.SwitchCurrentActionMap("Gameplay");
        IsDead = false;
        m_Animator.SetBool("FadeIn", IsDead);


    }



    public void LoadBaseMenue() 
    {
        // SceneManager.LoadScene();

        // Boolean On BaseMenue Menue
    }
    public void LoadPauseMenue()
    {
        OldActionMapBuffer = m_PlayerInput.currentActionMap.name;

        
        // Boolean On pause Menue

        // SceneManager.LoadScene();
    }

    private void OnDeath() 
    {

        StartCoroutine(DeathPlayer());
        IsDead = false;
    }


    private void Awake()
    {
        m_PlayerInput= GetComponent<PlayerInput>();

        OnPlayerDeath.AddListener(OnDeath);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead) 
        {
            OnPlayerDeath.Invoke();

        }
    }
}
