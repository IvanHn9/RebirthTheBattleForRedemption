using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur_BattleState : EnemyState
{
    private Boss_Minotaur enemy;
    private Transform player;
    private int moveDir;
    private int randomAction;
    public Minotaur_BattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss_Minotaur enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
        AudioManager.instance.PlaySFX(21, enemy.transform);
        if (player.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(enemy.idleState);
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
                stateTimer = enemy.battleTime;
            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                {
                    stateMachine.ChangeState(enemy.attackState);
                }
            }
            else
                {
                    randomAction = Random.Range(1, 4);
                    switch (randomAction)
                    {
                        case 1:
                            stateMachine.ChangeState(enemy.earthquakeState);
                            break;
                        case 2:
                            stateMachine.ChangeState(enemy.chargeState);
                            
                            break;
                        case 3:
                            stateMachine.ChangeState(enemy.shockwaveState);
                            break;
                    default:
                        stateMachine.ChangeState(enemy.idleState);
                        break;
                    }
                }  
        }
        BattleStateFlipControl();
    }
    private void BattleStateFlipControl()
    {
        if (player.position.x > enemy.transform.position.x && enemy.facingDir == -1)
            enemy.Flip();
        else if (player.position.x < enemy.transform.position.x && enemy.facingDir == 1)
            enemy.Flip();
    }
    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.attackCooldown = Random.Range(enemy.minAttackCooldown, enemy.maxAttackCooldown);
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
}
