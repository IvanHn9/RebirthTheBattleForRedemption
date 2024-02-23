using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    private Rigidbody2D rb;
    private int direction;
    private float speed;
    private int damage;
    private bool canMove;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }
    void Update()
    {
        if (canMove)
            rb.velocity = new Vector2(direction,0) * speed;
    }
    public void SetupWave(int dir,float speed, int damage)
    {
        direction = dir;
        this.speed = speed;
        this.damage = damage;
        Destroy(gameObject, 3);

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
    }
}