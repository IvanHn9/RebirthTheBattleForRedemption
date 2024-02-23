using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDemon_IdleState : EnemyState
{
    private Enemy_FlyDemon enemy;

    public FlyDemon_IdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Enemy_FlyDemon enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.playerDetected)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
