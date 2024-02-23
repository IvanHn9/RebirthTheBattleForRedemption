using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur_IdleState : EnemyState
{
    private Boss_Minotaur enemy;
    private bool isPlayerEngage;
    private Transform player;
    public Minotaur_IdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss_Minotaur enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleTime;
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        isPlayerEngage = enemy.arena.GetComponent<Arena>().isPlayerSurrounding;
        if (stateTimer<0 && isPlayerEngage && !player.GetComponent<PlayerStats>().isDead)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
