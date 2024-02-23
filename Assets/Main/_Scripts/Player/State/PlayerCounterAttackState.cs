using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    private bool canCreateClone;
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        canCreateClone = true;
        SkillManager.instance.parry.canBeHeal = true;
        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("SuccessfulCounterAttack", false);
       
    }

    public override void Exit()
    {
        base.Exit();
        SkillManager.instance.parry.canBeHeal = false;
        player.stats.MakeInvincible(false);
    }

    public override void Update()
    {
        base.Update();
        player.SetZeroVelocity();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {

            if (hit.GetComponent<Arrow_Controller>() != null)
            {
                hit.GetComponent<Arrow_Controller>().FlipArrow();
                SuccesfulCounterAttack();
                player.skill.parry.UseSkill();
            }
            else if (hit.GetComponent<WizardBulletController>() != null)
            {
                hit.GetComponent<WizardBulletController>().DestroyMe();
                SuccesfulCounterAttack();
                player.skill.parry.UseSkill();
            }

            else if (hit.GetComponent<Enemy>() != null)
            {
                //if (hit.GetComponent<Enemy>().CanBeStunned())
                //{
                    SuccesfulCounterAttack();

                    
                    if (canCreateClone)
                    {
                        canCreateClone = false;
                        player.skill.parry.MakeMirageOnParry(hit.transform);
                    }
                //}
            }
        }
        if (stateTimer < 0 || triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
    private void SuccesfulCounterAttack()
    {
        stateTimer = 10;
        player.anim.SetBool("SuccessfulCounterAttack", true);
        player.stats.MakeInvincible(true);
        
    }
}
