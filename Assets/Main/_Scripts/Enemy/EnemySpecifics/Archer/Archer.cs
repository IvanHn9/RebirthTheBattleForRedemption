using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{
    [Header("Archer spisifc info")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpeed;
    [SerializeField] private float arrowDamage;

    public Vector2 jumpVelocity;
    public float jumpCooldown;
    public float safeDistance;
    [HideInInspector] public float lastTimeJumped;

    [Header("Additional collision check")]
    [SerializeField] private Transform groundBehindCheck;
    [SerializeField] private Vector2 groundBehindCheckSize;

    #region States
    public Archer_IdleState idleState { get; private set; }
    public Archer_MoveState moveState { get; private set; }
    public Archer_BattleState battleState { get; private set; }
    public Archer_AttackState attackState { get; private set; }
    public Archer_JumpState jumpState { get; private set; }
    public Archer_DeadState deadState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        idleState = new Archer_IdleState(this,stateMachine,"Idle",this);
        moveState = new Archer_MoveState(this, stateMachine, "Move", this);
        battleState = new Archer_BattleState(this, stateMachine, "Idle", this);
        attackState = new Archer_AttackState(this, stateMachine, "Attack", this);
        jumpState = new Archer_JumpState(this, stateMachine, "Jump", this);
        deadState = new Archer_DeadState(this, stateMachine, "Dead", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(moveState);
    }

    protected override void Update()
    {
        base.Update();
    }
    public override bool CanBeStunned()
    {
        //if (base.CanBeStunned())
        //{
        //TODO:stunstate
        //stateMachine.ChangeState(stunnedState);
        //return true;
        // }

        // return false;

        return true;
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
    public override void AnimationSpecialAttackTrigger()
    {
        
        GameObject newArrow = Instantiate(arrowPrefab, attackCheck.position, transform.rotation);
        newArrow.GetComponent<Arrow_Controller>().SetupArrow(arrowSpeed * facingDir, stats);
        newArrow.GetComponent<Arrow_Controller>().targetLayerName = "Player";
    }
    public bool GroundBehind() => Physics2D.BoxCast(groundBehindCheck.position, groundBehindCheckSize, 0, Vector2.zero, 0, whatIsGround);
    public bool WallBehind() => Physics2D.Raycast(wallCheck.position, Vector2.right * -facingDir, wallCheckDistance + 2, whatIsGround);
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireCube(groundBehindCheck.position, groundBehindCheckSize);
    }
}
