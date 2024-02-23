using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastSpellState : PlayerState
{
    public PlayerCastSpellState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetZeroVelocity();
        AudioManager.instance.PlaySFX(45, null);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
