using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellSystem {
    public class SpellProjectile : MonoBehaviour
    {
        public GameObject impactPrefab;
        public float damageMultiplier;
        public float projectileSpeed;
        public CollisionType collisionType;
        public bool spawnedByPlayer;
        public float spellLifetime = 10f;
        public float spellStart;
        public void Initialize()
        {
            spellStart = Time.time;
        }
        private void OnTriggerEnter(Collider other)
        {
            OnHit(other);
        }
        public virtual void OnHit(Collider other)
        {
            Debug.Log("Hit: " + other.gameObject.name);
        }
        public void SpawnImpact(Vector3 targetPosition)
        {
            SpellImpact impact = Instantiate(impactPrefab).GetComponent<SpellImpact>();
            impact.transform.position = targetPosition;
            impact.damageMultiplier = damageMultiplier;
            impact.collisionType = collisionType;
            impact.spawnedByPlayer = spawnedByPlayer;
        }
        private void Update()
        {
            if (Time.time - spellStart >= spellLifetime)
                Destroy(gameObject);
        }
    }
}