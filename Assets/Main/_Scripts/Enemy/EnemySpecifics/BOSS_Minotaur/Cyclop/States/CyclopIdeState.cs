using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopIdeState : EnemyState
{
    private Cyclop enemy;
    public CyclopIdeState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Cyclop enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(enemy.Arena.GetComponent<Arena>().isPlayerSurrounding)
        {
            stateMachine.ChangeState(enemy.BattleState);
        }
    }
}
