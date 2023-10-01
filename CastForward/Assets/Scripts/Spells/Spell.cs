using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellSystem
{
    [CreateAssetMenu(fileName = "SpellObject", menuName = "Spell")]
    public class Spell : Item
    {
        public delegate void SpellPickup(Spell spell);
        public static event SpellPickup OnSpellPickup;
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private GameObject _impactPrefab;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private float _damageMultiplier;
        [SerializeField] private CollisionType _collisionType;

        public float manaCost;

        public void SummonSpell(Vector3 spawnPosition, Quaternion direction, bool spawnedByPlayer)
        {
            SpellProjectile projectile = Instantiate(_projectilePrefab, spawnPosition, direction).GetComponent<SpellProjectile>();
            projectile.Initialize();
            projectile.projectileSpeed = _projectileSpeed;
            projectile.damageMultiplier = _damageMultiplier;
            projectile.collisionType = _collisionType;
            projectile.spawnedByPlayer = spawnedByPlayer;
            if (_impactPrefab != null)
                projectile.impactPrefab = _impactPrefab;
        }
        public override void Interact()
        {
            OnSpellPickup?.Invoke(this);
        }
    }
}