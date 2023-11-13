using SpellSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity

{
    [SerializeField] private string[] _hurtSounds;
    [SerializeField] private string _deathSound;
    public delegate void PlayerHPChange(float currentHP, float maxHitpoints);
    public delegate void PlayerDie();
    public delegate void PlayerHit();
    public static event PlayerHPChange OnPlayerHPChange;
    public static event PlayerDie OnPlayerDeath;
    public static event PlayerHit OnPlayerHit;
    private bool _isDead = false;
    private void Start()
    {
        OnPlayerHPChange?.Invoke(CurrentHP, MaxHitpoints);
    }
    public override void GetHit(float amount)
    {
        if (_isDead) return;
        int randomIndex = Random.Range(0, _hurtSounds.Length);

        base.GetHit(amount);
        OnPlayerHit?.Invoke();
        OnPlayerHPChange?.Invoke(CurrentHP, MaxHitpoints);
        if (CurrentHP <= 0) Die();
        else AudioManager.Instance.PlaySound(_hurtSounds[randomIndex], AudioType.SFX, transform);
    }

    public override void GetHit(float amount, bool canStagger)
    {
        base.GetHit(amount, canStagger);
        OnPlayerHit?.Invoke();
        OnPlayerHPChange?.Invoke(CurrentHP, MaxHitpoints);
    }

    public override void GetHit(float amount, bool canHitPlayer, ISpellEffect spellEffect)
    {
        if (canHitPlayer)
        {
            base.GetHit(amount, canHitPlayer, spellEffect);
            OnPlayerHit?.Invoke();
            OnPlayerHPChange?.Invoke(CurrentHP, MaxHitpoints);
        }
    }
    public override void Die()
    {
        AudioManager.Instance.PlaySound(_deathSound, AudioType.SFX, transform);
        _isDead = true;
        OnPlayerDeath?.Invoke();
    }
}
