using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclop_MoveState : EnemyState
{
    private Cyclop enemy;
    private Transform player;
    public Cyclop_MoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Cyclop enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(enemy.moveSpeed*enemy.facingDir, rb.velocity.y);
        if(Vector3.Distance(player.transform.position, enemy.transform.position)<enemy.attackCheckRadius)
        {
            
            stateMachine.ChangeState(enemy.MeleeAttackState);
        }
        else if(stateTimer < 0 )
        {
            enemy.SetZeroVelocity();
            stateMachine.ChangeState(enemy.BattleState);
        }
        else if(player.transform.position.x < enemy.transform.position.x && enemy.facingDir == 1)
        {
            enemy.Flip();
        }
        else if (player.transform.position.x > enemy.transform.position.x && enemy.facingDir == -1)
        {
            enemy.Flip();
        }
        else if( enemy.IsWallDetected() || !enemy.IsGroundDetected()) 
        {
            enemy.Flip();
        }

    }
}
