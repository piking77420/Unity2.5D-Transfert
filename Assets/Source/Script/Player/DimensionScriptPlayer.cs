using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionScriptPlayer : DimensionScript
{

    [Header("TimeLimiteOnOtherDimension")]

    [SerializeField]
    public float MaxTimeForOtherDimension;

    [SerializeField]
    public float CurrentForOtherDimension;



    private PlayerStatus m_PlayerStatus;

    protected override void Awake()
    {
        m_PlayerStatus = GetComponent<PlayerStatus>();
        base.Awake();
    }

    protected override void Start()
    {

        CurrentForOtherDimension = MaxTimeForOtherDimension;
        base.Start();
    }

    private void TimeLimit() 
    {
        if (m_PlayerStatus.IsDead) 
        {
            return;
        }

        if(CurrentDimension == Dimension.Special) 
        {
            CurrentForOtherDimension -= Time.deltaTime;

            if(CurrentForOtherDimension < 0) 
            {
                m_PlayerStatus.KillPlayer();
            }
        }

        if(CurrentDimension == Dimension.Normal) 
        {
            CurrentForOtherDimension = MaxTimeForOtherDimension;
        }
    }


    // Update is called once per frame
    protected override void Update()
    {
        TimeLimit();
        base.Update();
    }
}
