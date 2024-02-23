using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclop_ThrowObjectState : EnemyState
{
    private Cyclop enemy;
    public Cyclop_ThrowObjectState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Cyclop enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
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
        if(triggerCalled)
        {
            stateMachine.ChangeState(enemy.BattleState);
        }
    }
}
