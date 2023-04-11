using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThrowEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject EnemyTaken;



    [SerializeField]
    private Rigidbody m_Rigidbody;





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






    private bool m_IsPlayerAiming;

    private bool m_PlayerHasThrow;
    public void OnPlayerAiming(InputAction.CallbackContext _callbackContext)
    {

        if (true || EnemyTaken != null)
        {
            if (!_callbackContext.performed)
            {
                m_AimReadValue = Vector2.zero;


            }
            else 
            {
                m_AimReadValue = _callbackContext.ReadValue<Vector2>();

            }

        }
    }


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }





    private void UpdateProjectilePos()
    {


        Vector3 pos = gameObject.transform.position;
        Vector3 NormalizeVelocity = Vector3.Normalize(m_Rigidbody.velocity);

        Vector3 enemyPos = new Vector3(NormalizeVelocity.x * DistanceFromPlayer, DistanceFromPlayer, 0);

        EnemyTaken.transform.position = pos + enemyPos;

    }


    private void ThrowProjectile()
    {
        m_PlayerForce = -m_AimReadValue;
        // ForcAdded = new Vector3(m_PlayerForce.x * PlayerThrowForce, m_PlayerForce.y * PlayerThrowForce, 0);
        float baseMultiplicator = 10f;


        if (m_AimReadValue != Vector2.zero) 
        {
            ForcAdded = new Vector3(m_PlayerForce.x, m_PlayerForce.y , 0) * PlayerThrowForce * baseMultiplicator;

            m_IsPlayerAiming = true;
        }

        if(m_IsPlayerAiming && m_AimReadValue == Vector2.zero) 
        {

            Rigidbody rbEnemy = EnemyTaken.GetComponent<Rigidbody>();
            


            rbEnemy.useGravity = true;


            Debug.Log(ForcAdded);

            rbEnemy.AddForce(ForcAdded);

            EnemyTaken = null;
            m_PlayerHasThrow = true;
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
    }
}
