using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeHitbox : MonoBehaviour
{

    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public float attackDamage = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isAttacking)
        {
            other.GetComponent<PlayerEntity>().GetHit(attackDamage);
        }
    }
}
