using UnityEngine;


public class DeathBringerDeadState : EnemyState
{
    private Enemy_DeathBringer enemy;

    public DeathBringerDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_DeathBringer enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.cd.enabled = false;
        
        stateTimer = .2f;
        AudioManager.instance.PlaySFX(11, enemy.transform);
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer<0)
        {
            rb.isKinematic = true;
        }
    }
}
