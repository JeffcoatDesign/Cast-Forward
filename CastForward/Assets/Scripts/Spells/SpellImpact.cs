using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellSystem
{
    public class SpellImpact : MonoBehaviour
    {
        public float damageMultiplier;
        public CollisionType collisionType;
        public bool spawnedByPlayer;
        private float _spellLifetime = 3f;
        private float _spellStart;
        private void Start()
        {
            _spellStart = Time.time;
        }
        private void OnTriggerEnter(Collider other)
        {
            OnHit(other);
        }
        public virtual void OnHit(Collider other)
        {
            Debug.Log("Hit: " + other.name);
        }
        private void Update()
        {
            if (Time.time - _spellStart >= _spellLifetime)
                Destroy(gameObject);
        }
    }
}