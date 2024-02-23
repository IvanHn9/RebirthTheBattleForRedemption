using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FT_AttackState : EnemyState
{
    private E_FierceTooth enemy;
    public FT_AttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, E_FierceTooth enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();
        enemy.SetZeroVelocity();
        AudioManager.instance.PlaySFX(39, enemy.transform);
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
            stateMachine.ChangeState(enemy.idleState);
    }

}
