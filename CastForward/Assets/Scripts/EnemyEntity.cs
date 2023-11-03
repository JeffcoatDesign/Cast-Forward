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
    public bool isAlive = true;
    public override void GetHit(float amount)
    {
        base.GetHit(amount);
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
