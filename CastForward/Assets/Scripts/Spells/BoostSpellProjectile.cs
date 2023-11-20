using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellSystem
{
    public class BoostSpellProjectile : SpellProjectile
    {
        private Rigidbody rb;
        private void Start()
        {
            rb = FindFirstObjectByType<PlayerEntity>().GetComponent<Rigidbody>();
            StartCoroutine(Boost());
        }
        public override void OnHit(Collider other)
        {
        }

        IEnumerator Boost ()
        {
            float startTime = Time.time;
            while (Time.time - startTime < spellLifetime - 0.1f)
            {
                rb.AddForce(rb.transform.forward * damageMultiplier);
                yield return new WaitForFixedUpdate();
            }
            yield return null;
        }
    }
}