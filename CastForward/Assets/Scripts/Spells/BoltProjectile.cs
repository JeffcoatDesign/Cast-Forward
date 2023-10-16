using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellSystem
{
    public class BoltProjectile : SpellProjectile
    {
        private Rigidbody _rb;
        private bool hasHit = false;
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }
        public override void OnHit(Collider other)
        {
            bool canHitPlayer = (!spawnedByPlayer && (collisionType == CollisionType.Enemy || collisionType == CollisionType.Neutral)) || (spawnedByPlayer && (collisionType == CollisionType.Ally || collisionType == CollisionType.Neutral));
            bool canHitEnemies = (!spawnedByPlayer && (collisionType == CollisionType.Ally || collisionType == CollisionType.Neutral)) || (spawnedByPlayer && (collisionType == CollisionType.Enemy || collisionType == CollisionType.Neutral));

            if (!hasHit)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    if (canHitPlayer)
                    {
                        SpawnImpact(transform.position);
                        Destroy(gameObject);
                    }
                }
                else if (other.gameObject.CompareTag("Enemy"))
                {
                    if (canHitEnemies)
                    {
                        SpawnImpact(transform.position);
                        Destroy(gameObject);
                    }
                }
                else if (other.gameObject.CompareTag("Projectile"))
                    return;
                else
                {
                    SpawnImpact(transform.position);
                    Destroy(gameObject);
                }
            }
        }
        private void Update()
        {
            if (_rb != null)
            {
                _rb.AddForce(transform.forward * projectileSpeed * Time.deltaTime, ForceMode.Force);
            }
            if (Time.time - spellStart >= spellLifetime)
                Destroy(gameObject);
        }
    }
}