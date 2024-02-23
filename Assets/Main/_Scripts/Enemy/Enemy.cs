using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EntityFx))]
public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;


    [Header("Stunned info")]
    public float stunDuration = 1;
    public Vector2 stunDirection = new Vector2(10,12);
    protected bool canBeStunned;
    //[SerializeField] protected GameObject counterImage;

    [Header("Move info")]
    public float moveSpeed = 1.5f;
    public float idleTime = 2;
    public float battleTime = 7;
    private float defaultMoveSpeed;

    [Header("Attack info")]
    public float agroDistance = 2;
    public float attackDistance = 2;
    public float attackCooldown;
    public float minAttackCooldown = 1;
    public float maxAttackCooldown= 2;
    [HideInInspector] public float lastTimeAttacked;

    public EnemyStateMachine stateMachine { get; private set; }
    public EntityFx fx { get; private set; }
    protected Transform player;
    //private Player player;
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();

        defaultMoveSpeed = moveSpeed;
        
    }


    protected override void Start()
    {
        base.Start();

        fx = GetComponent<EntityFx>();
        player = PlayerManager.instance.player.transform;
    }

    protected override void Update()
    {
        base.Update();


        stateMachine.currentState.Update();


    }
    public override void SlowEntityBy(float _slowPercentage, float _slowDuration)
    {
        moveSpeed = moveSpeed * (1 - _slowPercentage);
        anim.speed = anim.speed * (1 - _slowPercentage);

        Invoke("ReturnDefaultSpeed", _slowDuration);
    }

    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();

        moveSpeed = defaultMoveSpeed;
    }

    public virtual void FreezeTime(bool _timeFrozen)
    {
        if (_timeFrozen)
        {
            moveSpeed = 0;
            anim.speed = 0;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
            anim.speed = 1;
        }
    }

    public virtual void FreezeTimeFor(float _duration) => StartCoroutine(FreezeTimerCoroutine(_duration));

    protected virtual IEnumerator FreezeTimerCoroutine(float _seconds)
    {
        FreezeTime(true);

        yield return new WaitForSeconds(_seconds);

        FreezeTime(false);
    }
    //TODO: ---- Counter Attack ----
    #region Counter Attack Window
    //public virtual void OpenCounterAttackWindow()
    //{
    //    canBeStunned = true;
    //    counterImage.SetActive(true);
    //}

    //public virtual void CloseCounterAttackWindow()
    //{
    //    canBeStunned = false;
    //    counterImage.SetActive(false);
    //}
    #endregion
    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            //TODO: counterAttackwindow
            //CloseCounterAttackWindow();
            return true;
        }

        return false;
    }

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public virtual void AnimationSpecialAttackTrigger()
    {

    }

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(attackCheck.position, Vector2.right * facingDir, agroDistance, whatIsPlayer);
    public virtual RaycastHit2D IsPlayerBehindDetected() => Physics2D.Raycast(attackCheck.position, Vector2.left * facingDir, agroDistance, whatIsPlayer);
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + agroDistance * facingDir, transform.position.y));
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject,3f);
    }
}
