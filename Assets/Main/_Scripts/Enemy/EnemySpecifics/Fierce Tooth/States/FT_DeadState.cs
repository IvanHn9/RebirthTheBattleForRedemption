using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FT_DeadState : EnemyState
{
    private E_FierceTooth enemy;
    public FT_DeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, E_FierceTooth enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();
        stateTimer = .3f;
        AudioManager.instance.PlaySFX(33, enemy.transform);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0 && enemy.IsGroundDetected())
        {
            enemy.SetZeroVelocity();
            rb.isKinematic = true;
            enemy.cd.enabled = false;

        }

    }

}
