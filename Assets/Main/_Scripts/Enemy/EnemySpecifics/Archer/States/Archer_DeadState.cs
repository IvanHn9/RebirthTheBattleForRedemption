using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_DeadState : EnemyState
{
    private Archer enemy;
    public Archer_DeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Archer enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(32, enemy.transform);
        stateTimer = .3f;
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0 && enemy.IsGroundDetected())
        {
            enemy.SetZeroVelocity();
            rb.isKinematic = true;
            enemy.cd.enabled = false;
            
        }
    }
}
