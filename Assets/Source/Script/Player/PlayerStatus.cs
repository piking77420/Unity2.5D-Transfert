using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayerStatus : MonoBehaviour
{
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


    [HideInInspector,SerializeField]
    public UnityEvent OnPlayerDeath;

    [SerializeField]
    private float TimerFadeDeath;


    public int currentCheckpointIndex;


    [SerializeField]
    public bool IsInvicible;




    [SerializeField]
    private FadeSystem fadeSystem;


    [SerializeField]
    private Animator m_AnimatorForTranslate;

    private void ResetPos() 
    {
        m_DimensionScriptPlayer.transform.position = new Vector3(PlayerCurrentCheckpoint.x, PlayerCurrentCheckpoint.y, 0);


        if (m_DimensionScriptPlayer.CurrentDimension == DimensionScript.Dimension.Special) 
        {
            m_Animator.SetTrigger("Translate");
            m_Animator.SetInteger("Dimension",(int)DimensionScript.Dimension.Special);
        }

    }

    IEnumerator DeathPlayer()
    {

        m_PlayerInput.SwitchCurrentActionMap("None");
        yield return new WaitForSeconds(TimerFadeDeath/2f);
        ResetPos();
        yield return new WaitForSeconds(TimerFadeDeath/2f);
        m_PlayerInput.SwitchCurrentActionMap("Gameplay");


    }

    public void KillPlayer() 
    {
        if (!IsInvicible) 
        {
            IsDead = true;
        }
    }





    private void Awake()
    {
        m_PlayerInput= GetComponent<PlayerInput>();


        fadeSystem = FindObjectOfType<FadeSystem>();
        OnPlayerDeath.AddListener(fadeSystem.OnPlayerDeathFadeSystem);

        m_DimensionScriptPlayer = GetComponent<DimensionScriptPlayer>();
        m_AnimatorForTranslate = GetComponentInParent<Animator>(); 
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead)
        {
            IsDead = false;
            StartCoroutine(DeathPlayer());
            OnPlayerDeath?.Invoke();
            Debug.Log("ds");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
