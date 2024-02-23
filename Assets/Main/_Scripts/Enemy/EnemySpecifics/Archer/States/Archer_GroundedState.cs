using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_GroundedState : EnemyState
{
    protected Archer enemy;
    protected Transform player;

    public Archer_GroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Archer enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.transform.position) < enemy.agroDistance)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
