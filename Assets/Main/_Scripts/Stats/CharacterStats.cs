
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    damage,
    critChance,
    critPower,
    health
}
public class CharacterStats : MonoBehaviour
{
    private EntityFx fx;

    [Header("Major stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower;
    public Stat maxHealth;

    public int currentHealth;

    public System.Action onHealthChanged;
    public bool isDead { get; private set; }
    public bool isInvincible { get; set; }
    private bool isVulnerable;
    private bool criticalStrike;

    protected virtual void Start()
    {
        currentHealth = GetMaxHealthValue();
        critPower.SetDefaultValue(150);

        fx = GetComponent<EntityFx>();
    }
    protected virtual void Update()
    {

    }
    public void MakeVulnerableFor(float _duration) => StartCoroutine(VulnerableCorutine(_duration));

    private IEnumerator VulnerableCorutine(float _duartion)
    {
        isVulnerable = true;

        yield return new WaitForSeconds(_duartion);

        isVulnerable = false;
    }
    public virtual void DoDamage(CharacterStats _targetStats)
    {
        criticalStrike = false;
        if (_targetStats.isInvincible)
            return;

        _targetStats.GetComponent<Entity>().SetupKnockbackDir(transform);

        int totalDamage = damage.GetValue();
        if (CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
            criticalStrike = true;
        }
        fx.CreateHitFx(_targetStats.transform, criticalStrike);
        _targetStats.TakeDamage(totalDamage);
    }

    public virtual void TakeDamage(int _damage)
    {
        if (isInvincible)
            return;

        DecreaseHealthBy(_damage);
        
        GetComponent<Entity>().DamageImpact();
        fx.StartCoroutine("FlashFX");

        if (currentHealth <= 0 && !isDead)
            Die();
    }
    // ---- TANG MAU ----
    public virtual void IncreaseHealthBy(int _amount)
    {
        currentHealth += _amount;

        if (currentHealth > GetMaxHealthValue())
            currentHealth = GetMaxHealthValue();

        if (onHealthChanged != null)
            onHealthChanged();
    }
    // ---- GIAM MAU ----
    protected virtual void DecreaseHealthBy(int _damage)
    {

        if (isVulnerable)
            _damage = Mathf.RoundToInt(_damage * 1.1f);

        currentHealth -= _damage;
        if (_damage > 0)
        {
            //TODO: fix
            if (criticalStrike)
            {
                fx.CreatePopUpText(_damage.ToString(), Color.red);
            }
            else
            {
                fx.CreatePopUpText(_damage.ToString(), Color.white);
            }
            
        }
        if (onHealthChanged != null)
            onHealthChanged();
    }
    protected virtual void Die()
    {
        isDead = true;
    }
    // --- Giet ---
    public void KillEntity()
    {
        if (!isDead)
            Die();
    }
    // ---- BAT TU ----
    public void MakeInvincible(bool _invincible) => isInvincible = _invincible;

    // --- MAX HEALTH ---
    public int GetMaxHealthValue()
    {
        //return maxHealth.GetValue() + vitality.GetValue() * 5;
        return maxHealth.GetValue();
    }
    protected bool CanCrit()
    {
        int totalCriticalChance = critChance.GetValue();

        if (Random.Range(0, 100) <= totalCriticalChance)
        {
            return true;
        }


        return false;
    }
    protected int CalculateCriticalDamage(int _damage)
    {
        float totalCritPower = (critPower.GetValue()) * .01f;
        float critDamage = _damage * totalCritPower;

        return Mathf.RoundToInt(critDamage);
    }
    public Stat GetStat(StatType _statType)
    {

        if (_statType == StatType.damage) return damage;
        else if (_statType == StatType.critChance) return critChance;
        else if (_statType == StatType.critPower) return critPower;
        else if (_statType == StatType.health) return maxHealth;
        

        return null;
    }
}
