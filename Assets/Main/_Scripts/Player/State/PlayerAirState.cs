using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    private bool coyoteTime;
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        CheckCoyoteTime();
        if (Input.GetMouseButtonDown(0))
        {
            stateMachine.ChangeState(player.airAttack);
        }
        else if (yInput > 0 && player.jumpState.CanJump())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        else if (player.IsWallDetected())
        {
            stateMachine.ChangeState(player.wallSlide);
        }
        else if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
        if (xInput != 0) 
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
    }
    private void CheckCoyoteTime()
    {
        if(coyoteTime && Time.time > startTime + player.coyoteTimer)
        { 
            coyoteTime = false;
            player.jumpState.DecreaseAmountOfJump();
        }
    }
    public void StartCoyoteTime() => coyoteTime=true;
}
