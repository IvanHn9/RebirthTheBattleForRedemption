using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Wizard : Enemy
{
    #region uniqueInfo

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    #endregion
    #region States
    public Wizard_IdleState idleState { get; private set; }
    public Wizard_AttackState attackState { get; private set; }
    public Wizard_BattleState battleState { get; private set; }
    public Wizard_MoveState moveState { get; private set; }
    public Wizard_DeadState deadState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        idleState = new Wizard_IdleState(this, stateMachine, "Idle", this);
        moveState = new Wizard_MoveState(this, stateMachine, "Move", this);
        battleState = new Wizard_BattleState(this, stateMachine, "Move", this);
        attackState = new Wizard_AttackState(this, stateMachine, "Attack", this);
        deadState = new Wizard_DeadState(this, stateMachine, "Dead", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
    public override void AnimationSpecialAttackTrigger()
    {
        AudioManager.instance.PlaySFX(52, transform);
        GameObject bullet = Instantiate(bulletPrefab,attackCheck.transform.position,transform.rotation);
        WizardBulletController bulletScript = bullet.GetComponent<WizardBulletController>();
        bulletScript.SetUpBullet(facingDir, bulletSpeed, stats);
    }
}
