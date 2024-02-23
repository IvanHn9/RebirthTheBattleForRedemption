using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_IdleState : EnemyState
{
    private Dummy _enemy;
    public Dummy_IdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Dummy enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        _enemy = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
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
