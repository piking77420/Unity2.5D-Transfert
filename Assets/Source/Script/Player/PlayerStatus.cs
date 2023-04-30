using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayerStatus : MonoBehaviour
{

  public enum PlayerDiedSource 
    {
        Corruption , FromEnemy
    }








    // Start is called before the first frame update

    [Header("Dependencies")]

    [SerializeField]
    private PlayerInput m_PlayerInput;

    [SerializeField]
    private DimensionScriptPlayer m_DimensionScriptPlayer;

    [SerializeField]
    private Animator m_Animator;

    [Header("Value")]

    [SerializeField]
    public bool IsDead;

    [SerializeField]
    public Vector3 PlayerCurrentCheckpoint;

    [SerializeField]
    public Vector3 EnemyRespownCheckpoint;


    public UnityEvent OnPlayerDeath;
    public UnityEvent OnRespawn;


    [SerializeField]
    private float TimerFadeDeath;


    public int currentCheckpointIndex;


    [SerializeField]
    public bool IsInvicible;


    [SerializeField]
    private FadeSystem fadeSystem;


    [SerializeField]
    private Animator m_AnimatorForTranslate;


    [SerializeField]
    private bool m_KillPlayer;

    [Header("PlayerDeathSound")]
    private AudioSource[] m_AudioSource;

    [SerializeField,Range(0,5)]
    private float DelayForPlayerSound;

    [SerializeField]
     private AudioClip[] m_AudioClipPlayerDied;

    private void ResetPos() 
    {
        m_DimensionScriptPlayer.transform.position = new Vector3(PlayerCurrentCheckpoint.x, PlayerCurrentCheckpoint.y, 0);


        if (m_DimensionScriptPlayer.CurrentDimension == DimensionScript.Dimension.Special) 
        {
            m_Animator.SetTrigger("Translate");
            m_Animator.SetInteger("Dimension",(int)DimensionScript.Dimension.Special);
        }

    }

    private void PlayPlayerDeathSound(PlayerDiedSource DyingSource) 
    {
        m_AudioSource[0].clip = m_AudioClipPlayerDied[(int)DyingSource];
        m_AudioSource[0].PlayDelayed(DelayForPlayerSound);
    }

    IEnumerator DeathPlayer(PlayerDiedSource DyingSource)
    {
        
        m_PlayerInput.SwitchCurrentActionMap("None");
        
        OnPlayerDeath.Invoke();
        PlayPlayerDeathSound(DyingSource);

       yield return new WaitForSeconds(TimerFadeDeath);
        ResetPos();
        yield return new WaitForSeconds(TimerFadeDeath);
        m_PlayerInput.SwitchCurrentActionMap("Gameplay");



        OnRespawn.Invoke();
        IsInvicible = false;
    }

    public void KillPlayer(PlayerDiedSource DyingSource) 
    {
        if (!IsInvicible && !IsDead) 
        {
            IsDead = true;

            IsInvicible = true;
            StartCoroutine(DeathPlayer(DyingSource));
            IsDead = false;

        }
    }





    private void Awake()
    {
        m_PlayerInput= GetComponent<PlayerInput>();

        m_Animator = GetComponentInParent<Animator>();
        fadeSystem = FindObjectOfType<FadeSystem>();
        OnPlayerDeath.AddListener(fadeSystem.OnDeathPlayer);

        m_DimensionScriptPlayer = GetComponent<DimensionScriptPlayer>();
        m_AnimatorForTranslate = GetComponentInParent<Animator>();
        m_AudioSource = GetComponents<AudioSource>();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead)
        {

            StartCoroutine(DeathPlayer(PlayerDiedSource.Corruption));
            IsDead = false;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        if (m_KillPlayer) 
        {
            IsDead = true;
            m_KillPlayer = false;
        }
    }
}
