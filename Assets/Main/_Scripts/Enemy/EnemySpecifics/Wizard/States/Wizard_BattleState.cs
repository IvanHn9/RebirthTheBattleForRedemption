using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_BattleState : EnemyState
{
    Enemy_Wizard enemy;
    private Transform player;
    public Wizard_BattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Wizard enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
        if (CanAttack())
        {
            stateMachine.ChangeState(enemy.attackState);
        }
        else if (enemy.IsWallDetected())
        {
            enemy.Flip();
            enemy.SetZeroVelocity();
        }
        else
            enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);
    }
    public bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.attackCooldown = Random.Range(enemy.minAttackCooldown, enemy.maxAttackCooldown);
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
