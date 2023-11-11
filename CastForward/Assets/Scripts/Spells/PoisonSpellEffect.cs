using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellSystem {
    public class PoisonSpellEffect : MonoBehaviour, ISpellEffect
    {
        private Entity _entity;
        [SerializeField] private float duration = 4f;
        [SerializeField] private float tickTime = 0.2f;
        [SerializeField] private float damage = 4;
        [SerializeField] private int maxStacks = 1;
        [SerializeField] private bool canStagger = false;
        public float Duration { get { return duration; } set { duration = value; } }
        public float TickTime { get { return tickTime; } set { tickTime = value; } }
        public float Damage { get { return damage; } set { damage = value; } }
        public int MaxStacks { get { return maxStacks; } set { maxStacks = value; } }
        public bool CanStagger { get { return canStagger; } set { canStagger = value; } }
        void Start()
        {
            _entity = GetComponent<Entity>();
            if (_entity != null)
                StartCoroutine(StartTicking());
        }

        IEnumerator StartTicking()
        {
            float startTime = Time.time;
            while (Time.time - startTime < duration)
            {
                _entity.GetHit(damage,canStagger);
                yield return new WaitForSeconds(tickTime);
            }
            Destroy(this);
        }

        public void Visit (ISpellEffect other)
        {
            tickTime = other.TickTime;
            damage = other.Damage;
            maxStacks = other.MaxStacks;
            duration = other.Duration;
        }
    }
}