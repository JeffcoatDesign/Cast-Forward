using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellSystem
{
    public class SpellTrap : MonoBehaviour
    {
        [SerializeField] private Spell _spellToEmit;
        [SerializeField] private Transform _emitterTransform;
        [SerializeField] private float _extraSpellDelay;
        private float _lastCastTime;

        
        void Update()
        {
            if (Time.time - _lastCastTime > (_spellToEmit.castDelay + _extraSpellDelay))
            {
                _spellToEmit.SummonSpell(_emitterTransform, false);
                _lastCastTime = Time.time;
            }
        }
    }
}