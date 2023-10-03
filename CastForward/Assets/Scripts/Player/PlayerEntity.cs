using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    public delegate void PlayerHPChange(float currentHP, float maxHitpoints);
    public delegate void PlayerDie();
    public static event PlayerHPChange OnPlayerHPChange;
    public static event PlayerDie OnPlayerDeath;
    private void Start()
    {
        OnPlayerHPChange?.Invoke(CurrentHP, MaxHitpoints);
    }
    public override void GetHit(float amount)
    {
        base.GetHit(amount);
        OnPlayerHPChange?.Invoke(CurrentHP, MaxHitpoints);
        if (CurrentHP <= 0) Die();
    }
    public override void Die()
    {
        OnPlayerDeath?.Invoke();
    }
}
