using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{

    public bool isDashing;
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isDashing = true;
        
        stateTimer = player.dashDuration;


        player.skill.dash.CloneOnDash();
        player.skill.dash.InvincibleOnDash(true);

    }

    public override void Exit()
    {
        base.Exit();
        player.SetZeroVelocity();
        isDashing=false;

        player.skill.dash.CloneOnArrival();
        player.skill.dash.InvincibleOnDash(false);
    }






    public override void Update()
    {
        base.Update();


        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlide);

        player.SetVelocity(player.dashSpeed * player.dashDir, 0);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);

        //player.fx.CreateAfterImage();
    }
}
