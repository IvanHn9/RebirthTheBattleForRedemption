using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_AttackState : EnemyState
{
    private Archer enemy;
    public Archer_AttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Archer enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(27, enemy.transform);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();



        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);
    }
}
