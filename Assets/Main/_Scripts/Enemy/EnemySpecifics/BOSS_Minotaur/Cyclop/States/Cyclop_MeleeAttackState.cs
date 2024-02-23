using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclop_MeleeAttackState : EnemyState
{
    private Cyclop enemy;
    public Cyclop_MeleeAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Cyclop enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(triggerCalled)
        {
            stateMachine.ChangeState(enemy.BattleState);
        }
    }
}
