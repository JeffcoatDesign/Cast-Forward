using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellSystem
{
    public class AnimatedSpellImpact : SpellImpact
    {
        public override void OnHit(Collider other)
        {
            Entity ent = other.GetComponent<Entity>();
            if (ent != null)
            {
                if (spawnedByPlayer && ent.CompareTag("Enemy"))
                    ent.GetHit(damageMultiplier, true);
                else if (!spawnedByPlayer && ent.CompareTag("Player"))
                    ent.GetHit(damageMultiplier, true);
            }
        }

        public void FinishAnim ()
        {
            Destroy(gameObject);
        }
    }
}