using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_DeadState : EnemyState
{
    Enemy_Wizard enemy;
    public Wizard_DeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Wizard enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetZeroVelocity();
        enemy.cd.enabled = false;
        AudioManager.instance.PlaySFX(20, enemy.transform);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
