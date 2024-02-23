using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur_AttackState : EnemyState
{
    private Boss_Minotaur enemy;
    public int comboCounter { get; private set; }

    public Minotaur_AttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss_Minotaur enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        comboCounter = 0;
        enemy.anim.SetInteger("ComboCounter", comboCounter);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        //if (stateTimer < 0)
        //    enemy.SetZeroVelocity();
        if(triggerCalled)
        {
            if (comboCounter == 2)
            {
                enemy.SetZeroVelocity();
                stateMachine.ChangeState(enemy.idleState);
                return;
            }
            triggerCalled = false;
            comboCounter++;
            enemy.anim.SetInteger("ComboCounter", comboCounter);
            enemy.SetVelocity(5 * enemy.facingDir, 0);
            

        }      
    }
}
