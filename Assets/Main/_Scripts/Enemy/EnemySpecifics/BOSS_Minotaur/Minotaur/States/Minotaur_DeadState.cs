using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur_DeadState : EnemyState
{
    private Boss_Minotaur enemy;
    public Minotaur_DeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss_Minotaur enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();

        stateTimer = .3f;
        enemy.cd.enabled = false;
        AudioManager.instance.PlaySFX(33, enemy.transform);
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
        }
    }
}
