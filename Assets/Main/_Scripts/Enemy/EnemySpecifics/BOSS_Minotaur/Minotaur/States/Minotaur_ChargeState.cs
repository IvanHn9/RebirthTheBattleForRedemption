using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur_ChargeState : EnemyState
{
    private Boss_Minotaur enemy;
    private bool isHit;
    public Minotaur_ChargeState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss_Minotaur enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.chargeTime;
        isHit = false;
        enemy.stats.isInvincible = true;
        
    }

    public override void Exit()
    {
        base.Exit();
        enemy.stats.isInvincible = false;
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);
        if(stateTimer < 0 || isHit)
        {
            enemy.SetZeroVelocity();
            stateMachine.ChangeState(enemy.idleState);
        }
        else if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.SetZeroVelocity();
            enemy.Flip();    
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius/2);
        foreach(Collider2D collider in colliders)
        {
            if (collider.GetComponent<Player>() != null && !isHit)
            {
                isHit = true;
                enemy.fx.ScreenShake(enemy.shakePower);
                PlayerStats target = collider.GetComponent<PlayerStats>();
                enemy.stats.DoDamage(target);
            }
        }
               
    }   
}
