using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellSystem
{
    public class SpraySpell : SpellProjectile
    {
        private ISpellEffect _spellEffect;
        private void Start()
        {
            _spellEffect = GetComponent<ISpellEffect>();
        }
        public override void OnHit(Collider other)
        {
            Entity entity = other.GetComponent<Entity>();
            if (entity != null)
            {
                bool canHitPlayer = !spawnedByPlayer;
                entity.GetHit(damageMultiplier, canHitPlayer,_spellEffect);
            }
        }
    }
}