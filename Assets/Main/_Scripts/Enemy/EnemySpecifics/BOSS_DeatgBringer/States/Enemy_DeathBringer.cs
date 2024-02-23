using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_DeathBringer : Enemy
{
    #region States
    public DeathBringerBattleState battleState { get ; private set; }
    public DeathBringerAttackState attackState { get ; private set; }
    public DeathBringerIdleState idleState { get ; private set; }
    public DeathBringerDeadState deadState { get ; private set; }
    public DeathBringerSpellCastState spellCastState { get ; private set; }
    public DeathBringerTeleportState teleportState { get ; private set; }

    #endregion
    public bool bossFightBegun;

    [Header("Spell cast details")]
    [SerializeField] private GameObject spellPrefab;
    public int amountOfSpells;
    public float spellCooldown;
    public float lastTimeCast;
    [SerializeField] private float spellStateCooldown;
    [SerializeField] private Vector2 spellOffset;

    [Header("Teleport details")]
    [SerializeField] private BoxCollider2D arena;
    [SerializeField] private Vector2 surroundingCheckSize;
    public float chanceToTeleport;
    public float defaultChanceToTeleport = 25;

    [SerializeField] private GameObject portalPreb;
    protected override void Awake()
    {
        base.Awake();

        SetupDefaultFacingDir(-1);

        idleState = new DeathBringerIdleState(this, stateMachine, "Idle", this);

        battleState = new DeathBringerBattleState(this, stateMachine, "Move", this);
        attackState = new DeathBringerAttackState(this, stateMachine, "Attack", this);

        deadState = new DeathBringerDeadState(this, stateMachine, "Dead", this);
        spellCastState = new DeathBringerSpellCastState(this, stateMachine, "CastSpell", this);
        teleportState = new DeathBringerTeleportState(this, stateMachine, "Teleport", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        if(stateMachine.currentState == idleState)
        {
            if (player.transform.position.x < transform.position.x && facingDir == 1)
                Flip();
            else if (player.transform.position.x > transform.position.x && facingDir == -1)
                Flip();
        }
    }
    public override void Die()
    {
        base.Die();
        
        stateMachine.ChangeState(deadState);
        StartCoroutine(Delay(2));
        
    }
    public override RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(groundCheck.position, Vector2.right * facingDir, agroDistance, whatIsPlayer);
    public void CastSpell()
    {
        Player player = PlayerManager.instance.player;


        float xOffset = 0;

        if (player.rb.velocity.x != 0)
            xOffset = player.facingDir * spellOffset.x;

        Vector3 spellPosition = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + spellOffset.y);

        GameObject newSpell = Instantiate(spellPrefab, spellPosition, Quaternion.identity);
        newSpell.GetComponent<DeathBringerSpell_Controller>().SetupSpell(stats);
        AudioManager.instance.StopSFX(45);
        AudioManager.instance.PlaySFX(45,transform);
    }

    public void FindPosition()
    {
        float x = Random.Range(arena.bounds.min.x + 2, arena.bounds.max.x - 2);
        float y = Random.Range(arena.bounds.min.y + 3, arena.bounds.max.y - 3);

        transform.position = new Vector3(x, y);
        transform.position = new Vector3(transform.position.x, transform.position.y - GroundBelow().distance + (cd.size.y / 2));

        if (!GroundBelow() || SomethingIsAround())
        {
            FindPosition();
        }
    }


    private RaycastHit2D GroundBelow() => Physics2D.Raycast(transform.position, Vector2.down, 100, whatIsGround);
    private bool SomethingIsAround() => Physics2D.BoxCast(transform.position, surroundingCheckSize, 0, Vector2.zero, 0, whatIsGround);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position, surroundingCheckSize);
    }


    public bool CanTeleport()
    {
        if (Random.Range(0, 100) <= chanceToTeleport)
        {
            chanceToTeleport = defaultChanceToTeleport;
            return true;
        }


        return false;
    }

    public bool CanDoSpellCast()
    {
        if (Time.time >= lastTimeCast + spellStateCooldown)
        {
            return true;
        }

        return false;
    }
    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        AudioManager.instance.PlayBGM(4);
        GameObject portal = Instantiate(portalPreb, transform.position, Quaternion.identity);
    }
}
