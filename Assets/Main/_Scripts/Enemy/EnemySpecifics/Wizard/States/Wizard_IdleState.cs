using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_IdleState : EnemyState
{
    private Enemy_Wizard enemy;
    private Transform player;
    public Wizard_IdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Enemy_Wizard enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;

    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
        enemy.SetZeroVelocity();
        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.IsPlayerDetected() || Vector3.Distance(enemy.transform.position,player.transform.position)<enemy.agroDistance)
        {
            stateMachine.ChangeState(enemy.battleState); 

        }
        else if (stateTimer < 0)
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.moveState);
        }
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
