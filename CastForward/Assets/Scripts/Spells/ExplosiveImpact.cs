using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellSystem
{
    public class ExplosiveImpact : SpellImpact
    {
        public override void OnHit(Collider other)
        {
            bool canHitPlayer = (!spawnedByPlayer && (collisionType == CollisionType.Enemy || collisionType == CollisionType.Neutral)) || (spawnedByPlayer && (collisionType == CollisionType.Ally || collisionType == CollisionType.Neutral));
            bool canHitEnemies = (!spawnedByPlayer && (collisionType == CollisionType.Ally || collisionType == CollisionType.Neutral)) || (spawnedByPlayer && (collisionType == CollisionType.Enemy || collisionType == CollisionType.Neutral));
            if (canHitPlayer && other.CompareTag("Player"))
            {
// TODO Deal damage to player
            }
            else if (canHitEnemies && other.CompareTag("Enemy"))
            {
// TODO: Deal damage to enemy
            }
        }
    }
}