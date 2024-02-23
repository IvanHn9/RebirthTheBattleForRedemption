using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;

    // drop system and soul

    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
    }

    // modify level stats
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }
    protected override void Die()
    {
        base.Die();

        enemy.Die();
        Destroy(gameObject, 3f);
        
        // generate drop
        // get souls
    }
}
