using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDemon_BattleState : EnemyState
{
    private Enemy_FlyDemon enemy;
    private Transform player;
    private Vector2 moveDir;
    public FlyDemon_BattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_FlyDemon enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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

        moveDir = player.transform.position - enemy.transform.position;
        moveDir.Normalize();
        if (enemy.playerDetected)
        {
            stateTimer = enemy.battleTime;

            if (Vector3.Distance(player.transform.position, enemy.transform.position)<enemy.attackDistance+1)
            {
                enemy.SetZeroVelocity();
                if (CanAttack())
                {
                    stateMachine.ChangeState(enemy.attackState);
                }

                else
                    stateMachine.ChangeState(enemy.idleState);
            }
        }
        if (Vector3.Distance(player.transform.position, enemy.transform.position) > enemy.agroDistance && stateTimer <0)
        {
            enemy.playerDetected = false;
            stateMachine.ChangeState(enemy.idleState);
        }
            //if (player.position.x > enemy.transform.position.x && enemy.facingDir == -1)
            //    enemy.Flip();
            //else if (player.position.x < enemy.transform.position.x && enemy.facingDir == 1)
            //    enemy.Flip();
            if (enemy.playerDetected && Vector3.Distance(player.transform.position,enemy.transform.position) <= enemy.attackDistance +1 )
            return;
        enemy.SetVelocity(enemy.moveSpeed * moveDir.x, enemy.moveSpeed*moveDir.y);
    }
    private bool CanAttack()
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
