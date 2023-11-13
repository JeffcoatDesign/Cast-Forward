using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellSystem
{
    public class FreezeSpellEffect : MonoBehaviour, ISpellEffect
    {
        private EnemyAI _ai;
        [SerializeField] private float speedModifier = 0.6f;
        [SerializeField] private float duration = 4f;
        [SerializeField] private float tickTime = 0.2f;
        [SerializeField] private float damage = 4;
        [SerializeField] private int maxStacks = 3;
        [SerializeField] private bool canStagger = false;
        public float Duration { get { return duration; } set { duration = value; } }
        public float TickTime { get { return tickTime; } set { tickTime = value; } }
        public float Damage { get { return damage; } set { damage = value; } }
        public int MaxStacks { get { return maxStacks; } set { maxStacks = value; } }
        public bool CanStagger { get { return canStagger; } set { canStagger = value; } }
        void Start()
        {
            _ai = GetComponent<EnemyAI>();
            if (_ai != null)
                StartCoroutine(StartTicking());
        }

        IEnumerator StartTicking()
        {
            float startTime = Time.time;
            _ai.ModifySpeed(speedModifier);
            while (Time.time - startTime < duration)
            {
                yield return new WaitForSeconds(tickTime);
            }
            _ai.ModifySpeed(1f / speedModifier);
            Destroy(this);
        }

        public void Visit(ISpellEffect other)
        {
            tickTime = other.TickTime;
            damage = other.Damage;
            maxStacks = other.MaxStacks;
            duration = other.Duration;
        }
    }
}