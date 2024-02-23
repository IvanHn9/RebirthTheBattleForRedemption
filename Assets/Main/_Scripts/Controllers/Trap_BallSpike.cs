using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_BallSpike : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Animator animator;
    private float speed;
    private int damage;
    private bool canMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
            rb.velocity = Vector2.down * speed;
    }
    public void SetupBall(float speed,int damage)
    {
        this.speed = speed;
        this.damage = damage;
        Destroy(gameObject, 4);

    }
    private void StuckInto(Collider2D collision)
    {
        if (collision.GetComponent<CharacterStats>()?.isInvincible == true)
            return;
        cd.enabled = false;
        animator.enabled = false;
        canMove = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = collision.transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterStats>()?.isInvincible == true)
            return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<CharacterStats>().TakeDamage(damage);
            collision.gameObject.GetComponent<Player>().SetupKnockbackDir(transform);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            StuckInto(collision);
    }
}
