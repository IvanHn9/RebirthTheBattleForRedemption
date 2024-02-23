using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        int num = Random.Range(1, 2);
        AudioManager.instance.PlaySFX(num,null);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {

                EnemyStats _target = hit.GetComponent<EnemyStats>();

                if(_target != null) 
                    player.stats.DoDamage(_target);

                //ItemData_Equipment weaponData = Inventory.instance.GetEquipment(EquipmentType.Weapon);

                //if (weaponData != null)
                //    weaponData.Effect(_target.transform);


            }
        }
    }
    private void ShootArrow()
    {
       SkillManager.instance.shootArrow.CreateArrow();
       UI_InGame.instance.SetCooldownOf(UI_InGame.instance.arrowImage);
       player.AnimationTrigger();
    }
    private void CastSpell()
    {
        SkillManager.instance.thunderStrike.CastThunderStrike();
        //UI_InGame.instance.SetCooldownOf(UI_InGame.instance.thund);
    }
}
