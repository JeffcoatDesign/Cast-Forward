using SpellSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity
{
    public delegate void EnemyDeath();
    public delegate void ThisDeath();
    public static event EnemyDeath OnEnemyDeath;
    public event ThisDeath OnDeath;
    public event ThisDeath OnResurrect;
    public event ThisDeath OnStagger;
    public bool isAlive = true;
    [SerializeField] private float staggerThreshold = 0.3f;
    public override void GetHit(float amount)
    {
        if (amount / CurrentHP > staggerThreshold)
            OnStagger?.Invoke();
        base.GetHit(amount);
    }
    public override void GetHit(float amount, bool canStagger)
    {
        if (canStagger && amount / CurrentHP > staggerThreshold)
            OnStagger?.Invoke();
        base.GetHit(amount);
    }
    public override void GetHit(float amount, bool canHitPlayer, ISpellEffect spellEffect)
    {
        if (((spellEffect != null) && spellEffect.CanStagger) && amount / CurrentHP > staggerThreshold)
            OnStagger?.Invoke();
        base.GetHit(amount, canHitPlayer, spellEffect);
    }
    public override void Die()
    {
        isAlive = false;
        OnEnemyDeath?.Invoke();
        OnDeath?.Invoke();
    }

    public void Resurrect ()
    {
        GetHeal(MaxHitpoints);
        isAlive = true;
        hasDied = false;
        OnResurrect?.Invoke();
    }
}
