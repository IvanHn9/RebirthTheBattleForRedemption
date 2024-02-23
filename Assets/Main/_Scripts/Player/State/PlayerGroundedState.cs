using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.jumpState.ResetAmountOfJump();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        // ---- BAN CUNG ----
        if (Input.GetKeyDown(KeyCode.Q) && player.skill.shootArrow.bowUnlocked)
            stateMachine.ChangeState(player.aimState);
        // ---- SKILL PARRY ----
        if (Input.GetKeyDown(KeyCode.Mouse1) && player.skill.parry.parryUnlocked)
        {
            if (player.skill.parry.cooldownTimer > 0)
            {
                player.fx.CreatePopUpText("Cooldown!", Color.red);
                return;
            }
            stateMachine.ChangeState(player.counterAttack);
        }
           

        if (Input.GetKeyDown(KeyCode.E) && player.skill.thunderStrike.unlockedThunderStrike)
        {
            if (player.skill.thunderStrike.cooldownTimer > 0)
            {
                player.fx.CreatePopUpText("Cooldown!", Color.red);
                return;
            }
            player.SetZeroVelocity();
            stateMachine.ChangeState(player.castSpellState);
        }
            

        // ---- CHANGE STATE ----
        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.primaryAttack);
        if (yInput > 0 && player.jumpState.CanJump())
        {

                stateMachine.ChangeState(player.jumpState);
            
        }
        else if (!player.IsGroundDetected())
        {
            player.airState.StartCoyoteTime();
            stateMachine.ChangeState(player.airState); 
        }
            


        
    }
}
