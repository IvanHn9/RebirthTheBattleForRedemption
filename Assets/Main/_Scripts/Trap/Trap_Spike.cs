using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Spike : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float cooldownTime;
    [SerializeField] float idleTime;
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
            StartCoroutine(Idle());
        }
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
    IEnumerator Idle()
    {
        yield return new WaitForSeconds(idleTime);
        animator.SetBool("Shoot", false);
        cooldownTimer = cooldownTime;
    }
}
