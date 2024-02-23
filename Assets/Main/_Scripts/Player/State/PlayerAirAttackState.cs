using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttackState : PlayerState
{
    public int comboCounter { get; private set; }

    private float lastTimeAttacked;
    private float comboWindow = 1;
    public PlayerAirAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (player.IsGroundDetected() || comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;

        player.anim.SetInteger("ComboCounter", comboCounter);


        float attackDir = player.facingDir;

        if (xInput != 0)
            attackDir = xInput;

        //---- Set Velocity When Attack ----
        player.SetVelocity(player.airAttackMovement[comboCounter].x * attackDir, player.airAttackMovement[comboCounter].y);


        stateTimer = .1f;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0 && comboCounter != 2)
            player.SetZeroVelocity();

        if (triggerCalled)
        {
            if(player.IsGroundDetected())
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.airState);
            }
        }
            
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .15f);

        comboCounter++;
        lastTimeAttacked = Time.time;
    }
}
