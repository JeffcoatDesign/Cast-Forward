using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public delegate void EntityDeath();
    public static event EntityDeath OnEntityDeath;
    [SerializeField] private float _maxHitpoints;
    private float _currentHitpoints;
    public float MaxHitpoints { get { return _maxHitpoints; } }
    public float CurrentHP { get { return _currentHitpoints; } }
    private void Awake()
    {
        _currentHitpoints = _maxHitpoints;
    }
    void Update()
    {
        if (_currentHitpoints <= 0)
        {
            OnEntityDeath?.Invoke();
            Die();
        }
    }

    public virtual void GetHit(float amount)
    {
        _currentHitpoints -= amount;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
