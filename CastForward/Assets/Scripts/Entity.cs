using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public delegate void VoidEvent();
    public delegate void HealthEvent(float current, float max);
    public static event VoidEvent OnEntityDeath;
    public event HealthEvent OnUpdateHP;
    [SerializeField] private float _maxHitpoints;
    private float _currentHitpoints;
    public bool hasDied = false;
    public float MaxHitpoints { get { return _maxHitpoints; } }
    public float CurrentHP { get { return _currentHitpoints; } }
    private void Awake()
    {
        _currentHitpoints = _maxHitpoints;
        OnUpdateHP?.Invoke(_currentHitpoints, _maxHitpoints);
    }
    void Update()
    {
        if (!hasDied && _currentHitpoints <= 0)
        {
            hasDied = true;
            OnEntityDeath?.Invoke();
            Die();
        }
    }

    public virtual void GetHit(float amount)
    {
        _currentHitpoints -= amount;
        OnUpdateHP?.Invoke(_currentHitpoints, _maxHitpoints);
        _currentHitpoints = Mathf.Clamp(_currentHitpoints, 0, _maxHitpoints);
    }
    public virtual void GetHit(float amount, bool canStagger)
    {
        _currentHitpoints -= amount;
        OnUpdateHP?.Invoke(_currentHitpoints, _maxHitpoints);
        _currentHitpoints = Mathf.Clamp(_currentHitpoints, 0, _maxHitpoints);
    }
    public virtual void GetHit(float amount, SpellSystem.ISpellEffect spellEffect)
    {
        _currentHitpoints -= amount;
        OnUpdateHP?.Invoke(_currentHitpoints, _maxHitpoints);
        if (amount > 0 && spellEffect != null)
        {
            if (GetComponents(spellEffect.GetType()).Length < spellEffect.MaxStacks)
            {
                SpellSystem.ISpellEffect newEffect = (SpellSystem.ISpellEffect)gameObject.AddComponent(spellEffect.GetType());
                newEffect.Visit(spellEffect);
            }
        }
        _currentHitpoints = Mathf.Clamp(_currentHitpoints, 0, _maxHitpoints);
    }
    public virtual void GetHeal(float amount)
    {
        _currentHitpoints += amount;
        _currentHitpoints = Mathf.Clamp(_currentHitpoints, 0, _maxHitpoints);
        OnUpdateHP?.Invoke(_currentHitpoints, _maxHitpoints);
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
