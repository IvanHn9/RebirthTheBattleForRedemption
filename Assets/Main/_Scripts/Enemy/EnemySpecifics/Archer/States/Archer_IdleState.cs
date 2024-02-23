using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_IdleState : Archer_GroundedState
{
    public Archer_IdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Archer enemy) : base(_enemyBase, _stateMachine, _animBoolName, enemy)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetZeroVelocity();
        rb.isKinematic = true;
        stateTimer = enemy.idleTime;
        
    }

    public override void Exit()
    {
        base.Exit();
        rb.isKinematic = false;
        
        //TODO:audio
        //AudioManager.instance.PlaySFX(14, enemy.transform);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.moveState);
    }
}
