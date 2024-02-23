using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclop_BattleState : EnemyState
{
    private Cyclop enemy;
    private Transform player;
    private int randomAction;
    public Cyclop_BattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Cyclop enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
        if (triggerCalled)
        {
            randomAction = Random.Range(0, 4);
            switch (randomAction)
            {
                case 1:
                    stateMachine.ChangeState(enemy.MoveState);
                    break;
                case 2:
                    stateMachine.ChangeState(enemy.ThrowObjectState);

                    break;
                case 3:
                    stateMachine.ChangeState(enemy.LaserState);
                    break;
                case 4:
                    stateMachine.ChangeState(enemy.LaserState);
                    break;
            }
            BattleStateFlipControl();
        }
        
    }
    private void BattleStateFlipControl()
    {
        if (player.position.x > enemy.transform.position.x && enemy.facingDir == -1)
            enemy.Flip();
        else if (player.position.x < enemy.transform.position.x && enemy.facingDir == 1)
            enemy.Flip();
    }
}
