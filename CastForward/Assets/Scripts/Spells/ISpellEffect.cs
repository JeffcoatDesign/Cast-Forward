using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellSystem
{
    public interface ISpellEffect
    {
        public float Duration { get; set; }
        public float TickTime { get; set; }
        public float Damage { get; set; }
        public int MaxStacks { get; set; }
        public bool CanStagger { get; set; }
        public void Visit(ISpellEffect other);
    }
}