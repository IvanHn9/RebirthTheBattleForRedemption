using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private int amountOfJumpsLeft;
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        amountOfJumpsLeft = player.amountOfJump;
    }

    public override void Enter()
    {
        base.Enter();
        amountOfJumpsLeft--;
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
        AudioManager.instance.PlaySFX(15, null);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0))
        {
            stateMachine.ChangeState(player.airAttack);
        }
        else if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.airState);
    }
    public bool CanJump()
    {
        if(amountOfJumpsLeft > 0)
            return true;
        else return false;
    }
    public void ResetAmountOfJump() => amountOfJumpsLeft = player.amountOfJump;
    public void DecreaseAmountOfJump() => amountOfJumpsLeft--;
}
