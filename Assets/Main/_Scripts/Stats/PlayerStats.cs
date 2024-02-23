using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;

    protected override void Start()
    {
        base.Start();

        player = GetComponent<Player>();
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }
    protected override void Die()
    {
        base.Die();
        player.Die();
        AudioManager.instance.PlaySFX(31, null);

    }
    protected override void DecreaseHealthBy(int _damage)
    {
        base.DecreaseHealthBy(_damage);

        if (isDead)
            return;
        AudioManager.instance.PlaySFX(31, null);
        if (_damage > GetMaxHealthValue() * .3f)
        {
            //player.SetupKnockbackPower(new Vector2(20, 6));

            // TODO: Player FX and audio
            //player.fx.ScreenShake(player.fx.shakeHighDamage);
            //int randomSound = Random.Range(34, 35);
            
        }
    }
    public void CloneDoDamage(CharacterStats _targetStats, float _multiplier)
    {

        int totalDamage = damage.GetValue();

        if (_multiplier > 0)
            totalDamage = Mathf.RoundToInt(totalDamage * _multiplier);

        if (CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }

        _targetStats.TakeDamage(totalDamage);
    }
}
