using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStrike_Controller : MonoBehaviour
{
    private BoxCollider2D cd;
    private void Awake()
    {
        cd = GetComponent<BoxCollider2D>();
        cd.enabled = false;
    }
    public void AttackTrigger()
    {
        cd.enabled = true;
    }
    public void AnimationTrigger()
    {
        cd.enabled = false;
        Destroy(gameObject);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            EnemyStats enemyTarget = collision.GetComponent<EnemyStats>();
            playerStats.DoDamage(enemyTarget);
        }
    }
}
