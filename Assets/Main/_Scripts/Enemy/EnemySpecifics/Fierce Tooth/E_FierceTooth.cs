using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_FierceTooth : Enemy
{
    #region States
    public FT_IdleState idleState { get; private set; }
    public FT_MoveState moveState { get; private set; }
    public FT_BattleState battleState { get; private set; }
    public FT_AttackState attackState { get; private set; }
    public FT_DeadState deadState { get; private set; }

    #endregion
    protected override void Awake()
    {
        base.Awake();
        idleState = new FT_IdleState(this, stateMachine, "Idle", this);
        moveState = new FT_MoveState(this, stateMachine, "Move", this);
        battleState = new FT_BattleState(this, stateMachine, "Move", this);
        attackState = new FT_AttackState(this, stateMachine, "Attack", this);
        deadState = new FT_DeadState(this, stateMachine, "Dead", this);

    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);

    }
    //public override bool CanBeStunned()
    //{
    //    if (base.CanBeStunned())
    //    {
    //        //stateMachine.ChangeState(stunnedState);
    //        return true;
    //    }

    //    return false;
    //}
}
