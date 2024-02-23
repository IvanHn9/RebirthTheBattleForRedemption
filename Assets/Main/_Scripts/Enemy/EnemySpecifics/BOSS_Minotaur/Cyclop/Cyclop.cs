using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclop : Enemy
{
    #region Info
    public Transform Arena;
    public GameObject PortalPrefab;
    #endregion
    #region States
    public CyclopIdeState IdleState { get; private set; }
    public Cyclop_MoveState MoveState { get; private set; }
    public Cyclop_BattleState BattleState { get; private set; }
    public Cyclop_DeadState DeadState { get; private set; }
    public Cyclop_LaserState LaserState { get; private set; }
    public Cyclop_MeleeAttackState MeleeAttackState { get; private set; }
    public Cyclop_ThrowObjectState ThrowObjectState { get; private set; }
    #endregion
    public override void AnimationSpecialAttackTrigger()
    {
        base.AnimationSpecialAttackTrigger();
    }

    public override void Die()
    {
        base.Die();
        GameObject portal = Instantiate(PortalPrefab,transform.position,Quaternion.identity);
    }

    protected override void Awake()
    {
        base.Awake();
        IdleState = new CyclopIdeState(this, stateMachine, "Idle", this);
        MoveState = new Cyclop_MoveState(this, stateMachine, "Move", this);
        BattleState = new Cyclop_BattleState(this, stateMachine, "Battle", this);
        LaserState = new Cyclop_LaserState(this, stateMachine, "Laser", this);
        ThrowObjectState = new Cyclop_ThrowObjectState(this, stateMachine, "Throw", this);
        MeleeAttackState = new Cyclop_MeleeAttackState(this, stateMachine, "MeleeAttack", this);
        DeadState = new Cyclop_DeadState(this, stateMachine, "Dead", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
