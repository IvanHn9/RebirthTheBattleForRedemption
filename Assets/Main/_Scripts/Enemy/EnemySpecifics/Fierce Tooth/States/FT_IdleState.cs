using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FT_IdleState : FT_GroundState
{
    public FT_IdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, E_FierceTooth enemy) : base(_enemyBase, _stateMachine, _animBoolName, enemy)
    {
    }
    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;

    }

    public override void Exit()
    {
        base.Exit();

     //   AudioManager.instance.PlaySFX(14, enemy.transform);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        { 

            if(Vector2.Distance(player.transform.position, enemy.transform.position) < 7)
                stateMachine.ChangeState(enemy.battleState);
            else
                stateMachine.ChangeState(enemy.moveState);
        }
            

    }
}
