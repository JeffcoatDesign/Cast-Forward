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
        public float castDelay = 0.1f;
        [SerializeField] private bool _isAnchored = false;
        [SerializeField] private bool _inheritVelocity = false;
        [SerializeField] private CollisionType _collisionType;

        public float manaCost;
        public float ProjectileSpeed { get { return _projectileSpeed; } }

        public void SummonSpell(Transform castPoint, Vector3 velocity, bool spawnedByPlayer)
        {
            SpellProjectile projectile = Instantiate(_projectilePrefab, castPoint.position, castPoint.rotation).GetComponent<SpellProjectile>();
            if (_isAnchored) projectile.transform.parent = castPoint;
            projectile.Initialize();
            projectile.projectileSpeed = _projectileSpeed;
            projectile.damageMultiplier = _damageMultiplier;
            projectile.collisionType = _collisionType;
            projectile.spawnedByPlayer = spawnedByPlayer;
            if (_impactPrefab != null)
                projectile.impactPrefab = _impactPrefab;

            if (_inheritVelocity)
            {
                Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
                if (projectileRB != null)
                {
                    projectileRB.velocity = velocity;
//TODO: Set velocity in right direction
                }
            }
        }
        public override void Interact()
        {
            OnSpellPickup?.Invoke(this);
        }
    }
}