using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_AttackState : EnemyState
{
    Enemy_Wizard enemy;
    private Transform player;
    public Wizard_AttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Wizard enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
        enemy.SetZeroVelocity();
        if (enemy.facingDir > 0 && player.transform.position.x < enemy.transform.position.x)
        {
            enemy.Flip();
        }
        else if (enemy.facingDir < 0 && player.transform.position.x > enemy.transform.position.x)
        {
            enemy.Flip();
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
        {
            
            stateMachine.ChangeState(enemy.idleState);
        }
            
    }
}
