using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Boss_Minotaur : Enemy
{
    #region BossInfo
    public BoxCollider2D arena;
    public Vector3 shakePower;
    public float chargeTime;
    public GameObject PortalPrefab;

    [Header("earthquake")]
    [SerializeField] private GameObject fallPrefab;
    [SerializeField] private float minFallSpeed;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private int fallDamage;
    [SerializeField] private int amount;

    [Header("Shockwave")]
    [SerializeField] private GameObject wavePrefab;
    [SerializeField] private float waveSpeed;
    [SerializeField] private int waveDamage;

    
    #endregion
    #region States
    public Minotaur_IdleState idleState { get; private set; }
    public Minotaur_BattleState battleState { get; private set; }
    public Minotaur_ChargeState chargeState { get; private set; }
    public Minotaur_EarthquakeState earthquakeState { get; private set; }
    public Minotaur_ShockwaveState shockwaveState { get; private set; } 
    public Minotaur_DeadState deadState { get; private set; }
    public Minotaur_AttackState attackState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        idleState = new Minotaur_IdleState(this, stateMachine, "Idle", this);
        battleState = new Minotaur_BattleState(this, stateMachine, "Battle", this);
        chargeState = new Minotaur_ChargeState(this, stateMachine, "Charge", this);
        earthquakeState = new Minotaur_EarthquakeState(this, stateMachine, "Earthquake", this);
        shockwaveState = new Minotaur_ShockwaveState(this, stateMachine, "Shockwave", this);
        deadState = new Minotaur_DeadState(this, stateMachine, "Dead", this);
        attackState = new Minotaur_AttackState(this, stateMachine, "Attack", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        
    }

    protected override void Update()
    {
        base.Update();
        if(stateMachine.currentState != chargeState)
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
        StartCoroutine(Delay(1));
        

    }
    public override void AnimationSpecialAttackTrigger()
    {
        base.AnimationSpecialAttackTrigger();
        fx.ScreenShake(shakePower);
        if (stateMachine.currentState == earthquakeState)
        {
            for (int i = 0; i < amount; i++) 
            {
                float x = Random.Range(arena.bounds.min.x + 2, arena.bounds.max.x - 2);
                float y = Random.Range(arena.bounds.max.y, arena.bounds.max.y +3);
                float fallSpeed = Random.Range(minFallSpeed, maxFallSpeed);
                GameObject fallingBall = Instantiate(fallPrefab, new Vector3(x,arena.bounds.max.y,0), Quaternion.identity);
                Trap_BallSpike fallingBallScript = fallingBall.GetComponent<Trap_BallSpike>();
                fallingBallScript.SetupBall(fallSpeed, fallDamage);
                StartCoroutine(SpawnFallingBallDelay(1));
            }   
        }
        if(stateMachine.currentState == shockwaveState)
        {
            GameObject shockWave = Instantiate(wavePrefab, transform.position+new Vector3(0,-.5f,0), transform.rotation);
            ShockWave shockWaveScript = shockWave.GetComponent<ShockWave>();
            shockWaveScript.SetupWave(facingDir,waveSpeed, waveDamage);
        }
    }
    IEnumerator SpawnFallingBallDelay(float time) 
    {
        yield return new WaitForSeconds(time);
    }
    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        GameObject portal = Instantiate(PortalPrefab, transform.position, Quaternion.identity);
    }
}
