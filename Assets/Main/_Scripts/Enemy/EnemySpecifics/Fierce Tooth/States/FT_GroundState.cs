using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FT_GroundState : EnemyState
{
    protected E_FierceTooth enemy;
    protected Transform player;
    public FT_GroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, E_FierceTooth enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
        if (!player.GetComponent<PlayerStats>().isDead)
        {
            if(enemy.IsPlayerBehindDetected())
            {
                
                stateMachine.ChangeState(enemy.moveState);
            }
        }
    }
}
