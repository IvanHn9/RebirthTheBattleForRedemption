using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum Direction
{
    Up, Down, Left, Right
}
public class Trap_Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D cd;
    private float speed;
    private int damage;
    private Direction dir;
    private Vector2 movedir;

    public bool canStuck;
    [SerializeField] private bool canMove;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<BoxCollider2D>();
        canMove = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
            rb.velocity = new Vector2(movedir.x *speed, movedir.y*speed);
    }
    public void SetUpArrow(Direction direction,float speed,int damage)
    {
        dir = direction;
        this.speed = speed;
        this.damage = damage;
        switch (dir) 
        {
            case Direction.Left:
                movedir = Vector2.left;
                transform.Rotate(0, 0, 90);
                break;
                case Direction.Right:
                movedir = Vector2.right;
                transform.Rotate(0, 0, -90);
                break;
                case Direction.Up:
                movedir = Vector2.up;
                break;
                case Direction.Down:
                movedir = Vector2.down;
                transform.Rotate(0, 0, 180);
                break;
        }
        Destroy(gameObject, 5);
    }
    private void StuckInto(Collider2D collision)
    {
        if(!canStuck) return;
        if (collision.GetComponent<CharacterStats>()?.isInvincible == true)
            return;
        cd.enabled = false;
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
            StuckInto(collision);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            StuckInto(collision);
    }
        
}
