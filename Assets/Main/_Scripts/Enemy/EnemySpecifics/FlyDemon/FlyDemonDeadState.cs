using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDemonDeadState : EnemyState
{
    private Enemy_FlyDemon enemy;
    public FlyDemonDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_FlyDemon enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.cd.enabled = false;
        rb.gravityScale = 1;
        AudioManager.instance.PlaySFX(20,enemy.transform);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(enemy.IsGroundDetected())
            rb.gravityScale = 0;
        //if(triggerCalled)
        //enemy.gameObject.SetActive(false);
    }
}
