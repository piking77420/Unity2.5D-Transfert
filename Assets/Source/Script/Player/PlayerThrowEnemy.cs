using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerThrowEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject EnemyTaken;

    [SerializeField]
    private Rigidbody m_Rigidbody;

    [SerializeField]

    private Transform m_PlayerTransform;

    [SerializeField]
    public Vector2 m_PlayerForce { get; private set; }


    [SerializeField, Range(1, 100)]
    private float PlayerThrowForce;

    [SerializeField]
    private Vector2 m_AimReadValue;

    [SerializeField]
    private float DistanceFromPlayer;


    [SerializeField]
    private Vector3 ForcAdded;



    [SerializeField,Range(2,4)]
    private float TimerToThrow;

    private float TimerToThrowCooldown;



    [SerializeField]
    private float angle;

    private bool m_IsPlayerAiming;
    private bool m_PlayerHasThrow;

    private Vector3 aimpos;

    private void OnDrawGizmos()
    {
        
        /*
        Vector3 pos = m_PlayerTransform.position;
        pos.x = pos.x + Mathf.Cos(angle) * 2 ;
        pos.y = pos.y + Mathf.Sin(angle) * 2;


        Gizmos.DrawWireSphere(pos, 1f);*/
    }

    public void OnPlayerAiming(InputAction.CallbackContext _callbackContext)
    {

        if ( EnemyTaken != null)
        {
            if (!_callbackContext.performed)
            {
                m_AimReadValue = Vector2.zero;


            }
            else 
            {
                m_AimReadValue = _callbackContext.ReadValue<Vector2>();


            }

            if (_callbackContext.canceled) 
            {
            }

        }



    }


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        TimerToThrowCooldown = TimerToThrow;
    }

    void Start()
    {

    }



    public void GetEnemy(InteractableObject interactableObject) 
    {
        if(interactableObject is EnemyPickable && TimerToThrow == TimerToThrowCooldown) 
        {
            EnemyTaken = interactableObject.gameObject;
            EnemyIsTaken(EnemyTaken.transform.parent.GetComponent<Rigidbody>());
            EnemyTaken.transform.position = this.transform.position;

            EnemyTaken.GetComponent<EnemyPatrol>().enabled = false;
            EnemyTaken.GetComponent<NavMeshAgent>().enabled = false;
            EnemyTaken.transform.parent.GetComponent<Animator>().enabled = false;
        }
    }



    private void UpdateProjectilePos()
    {

        Vector3 value;
            
        if(m_AimReadValue == Vector2.zero) 
        {
            value = Vector3.up;
        }
        else
        {
            value = (m_AimReadValue);
        }

        Vector3 nomalizeOne = m_AimReadValue.normalized;


        float currentAngle = Mathf.Atan2(nomalizeOne.y, nomalizeOne.x);
        angle = currentAngle;




        EnemyTaken.GetComponentInParent<Rigidbody>().useGravity = false; 

        Vector3 enemyPos = m_PlayerTransform.position;
        enemyPos.x = enemyPos.x + Mathf.Cos(currentAngle) * DistanceFromPlayer;
        enemyPos.y = enemyPos.y + Mathf.Sin(currentAngle) * DistanceFromPlayer;
    
        EnemyTaken.transform.position = enemyPos;


    }

    private void EnemyIsTaken(Rigidbody rb) 
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }



    private void ThrowProjectile()
    {
        m_PlayerForce = m_AimReadValue;

        float mag = Mathf.Clamp01(new Vector2(m_AimReadValue.x, m_AimReadValue.y).magnitude);


        float baseMultiplicator = 10f;

        Animator EnemyAnimator = EnemyTaken.GetComponentInParent<Animator>();
        Rigidbody EnemyRigidBody = EnemyTaken.GetComponentInParent<Rigidbody>();
        EnemyAnimator.enabled = false;


     

         if (!m_PlayerHasThrow && m_AimReadValue != Vector2.zero) 
         {


            ForcAdded = new Vector3(m_PlayerForce.x, m_PlayerForce.y , 0) * PlayerThrowForce * baseMultiplicator;


            m_IsPlayerAiming = true;
         }

        if (!m_PlayerHasThrow && m_IsPlayerAiming && mag == 1f)
        {




            EnemyTaken.GetComponentInParent<Rigidbody>().isKinematic = false;

            EnemyRigidBody.AddForce(ForcAdded * EnemyRigidBody.mass);
            ForcAdded = Vector3.zero;
            m_AimReadValue = Vector2.zero;

            EnemyTaken.TryGetComponent<EnemyThrowedBehaviour>(out EnemyThrowedBehaviour enemyThrowedBehaviour);



            enemyThrowedBehaviour.Is_Throwed = true;
            m_IsPlayerAiming = false;



            m_PlayerHasThrow = true;
            EnemyTaken = null;
        }

    }





    // Update is called once per frame
    void Update()
    {


        if (EnemyTaken != null)
        {
            
            Debug.Assert(EnemyTaken.GetComponent<EnemyPatrol>() != null);
            UpdateProjectilePos();
            ThrowProjectile();
        }

        if (m_PlayerHasThrow) 
        {
            if(TimerToThrow >= 0) 
            {
                TimerToThrow -= Time.deltaTime;
            }
            else 
            {
                m_PlayerHasThrow = false;
                TimerToThrow = TimerToThrowCooldown;
            }

        }
    }
}
