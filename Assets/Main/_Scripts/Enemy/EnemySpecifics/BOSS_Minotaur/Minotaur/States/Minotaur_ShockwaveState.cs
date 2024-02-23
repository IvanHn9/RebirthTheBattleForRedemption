using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur_ShockwaveState : EnemyState
{
    private Boss_Minotaur enemy;

    public Minotaur_ShockwaveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Boss_Minotaur enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(54, enemy.transform);
    }

    public override void Exit()
    {
        base.Exit();
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
