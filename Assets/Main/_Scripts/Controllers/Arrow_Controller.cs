using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Arrow_Controller : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D cd;
    private Player player;

    private float freezeTimeDuration;


    [SerializeField] public string targetLayerName;

    [SerializeField] private int damage;
    


    [SerializeField] private float xVelocity;

    [SerializeField] private bool canMove;
    [SerializeField] private bool flipped;

    [Header("Pierce info")]
    private float pierceAmount;


    private CharacterStats stats;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<BoxCollider2D>();
        canMove = true;
    }
    private void Update()
    {
        if(canMove)
            if(targetLayerName == "Player")
            rb.velocity = new Vector2(xVelocity,rb.velocity.y);
        else if(targetLayerName == "Enemy")
                transform.right = rb.velocity;
    }

    public void SetupArrow(float _speed,CharacterStats _stats)
    {
        xVelocity = _speed;
        stats = _stats;
    }
    public void SetupArrow(Vector2 _dir, float _gravityScale, CharacterStats _stats, float _freezeTimeDuration)
    {
        stats = _stats;
        freezeTimeDuration = _freezeTimeDuration;
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;

        Invoke("DestroyMe", 7);
    }
    private void DestroyMe()
    {
        Destroy(gameObject);
    }
    public void SetupPierce(int _pierceAmount)
    {
        pierceAmount = _pierceAmount;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterStats>()?.isInvincible == true)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer(targetLayerName))
        {

            //collision.GetComponent<CharacterStats>()?.TakeDamage(damage);

            stats.DoDamage(collision.GetComponent<CharacterStats>());
            StuckInto(collision);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            StuckInto(collision);
    }

    private void StuckInto(Collider2D collision)
    {
        if (collision.GetComponent<CharacterStats>()?.isInvincible == true)
            return;

        if (pierceAmount > 0 && collision.GetComponent<Enemy>() != null)
        {
            pierceAmount--;
            return;
        }
        //TODO: stop part
        //GetComponentInChildren<ParticleSystem>().Stop();
        cd.enabled = false;
        anim.enabled = false;
        canMove = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = collision.transform;

        Destroy(gameObject, Random.Range(5, 7));
    }

    public void FlipArrow()
    {
        if (flipped)
            return;


        xVelocity = xVelocity * -1;
        flipped = true;
        transform.Rotate(0, 180, 0);
        targetLayerName = "Enemy";
    }
}
