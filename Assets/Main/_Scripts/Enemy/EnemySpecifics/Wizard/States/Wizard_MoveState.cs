using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_MoveState : EnemyState
{
    private Enemy_Wizard enemy;
    private Transform player;
    
    public Wizard_MoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Wizard enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);
        if (enemy.IsPlayerDetected() || Vector3.Distance(enemy.transform.position, player.transform.position) < enemy.agroDistance)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
        if (enemy.IsWallDetected())
        {
            
            stateMachine.ChangeState(enemy.idleState);
        }
    }

}


