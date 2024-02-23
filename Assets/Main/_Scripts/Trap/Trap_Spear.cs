using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Spear : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float cooldownTime;

    private Animator animator;
    private float cooldownTimer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer < 0)
        {
            animator.SetBool("Shoot", true);
        }
    }
    public void AnimationFinishTrigger()
    {
        animator.SetBool("Shoot", false);
        cooldownTimer = cooldownTime; 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterStats>()?.isInvincible == true)
            return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<Player>().SetupKnockbackDir(transform);
            collision.gameObject.GetComponent<CharacterStats>().TakeDamage(damage);
        }
    }
}
