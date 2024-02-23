using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FlyDemon : Enemy
{
    #region States
    public FlyDemon_IdleState idleState { get; private set; }
    public FlyDemon_MoveState moveState { get; private set; }
    public FlyDemon_BattleState battleState { get; private set; }
    public FlyDemon_AttackState attackState { get; private set; }
    public FlyDemonDeadState deadState { get; private set; }

    #endregion

    #region info
    public bool playerDetected;
    #endregion
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }

    protected override void Awake()
    {
        base.Awake();
        SetupDefaultFacingDir(-1);
        idleState = new FlyDemon_IdleState(this, stateMachine, "Idle", this);
        moveState = new FlyDemon_MoveState(this, stateMachine,"Idle",this);
        battleState = new FlyDemon_BattleState(this, stateMachine, "Idle", this);
        attackState = new FlyDemon_AttackState(this, stateMachine, "Attack", this);
        deadState = new FlyDemonDeadState(this, stateMachine, "Dead", this);
        playerDetected = false;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position, agroDistance);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, agroDistance);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
                playerDetected = true;
        }
    }
}
