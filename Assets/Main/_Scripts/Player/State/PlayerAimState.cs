using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : PlayerState
{
    public PlayerAimState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        player.skill.shootArrow.DotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        player.skill.shootArrow.DotsActive(false);
        player.StartCoroutine("BusyFor", .2f);
    }

    public override void Update()
    {
        base.Update();
        player.SetZeroVelocity();
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (player.transform.position.x > mousePosition.x && player.facingDir == 1)
            player.Flip();
        else if (player.transform.position.x < mousePosition.x && player.facingDir == -1)
            player.Flip();

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.aimState);
            player.anim.SetBool("ShootBow", false);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            player.anim.SetBool("ShootBow", false);
            stateMachine.ChangeState(player.idleState);
        }
        else if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(player.skill.shootArrow.amountOfArrowLeft <= 0)
            {
                player.fx.CreatePopUpText("Out of Arrow!",Color.red);
                return;
            }
            player.anim.SetBool("ShootBow", true);
            AudioManager.instance.PlaySFX(27, player.transform);
        }    
    }
}
