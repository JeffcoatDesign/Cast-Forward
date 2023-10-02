using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathVoid : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            Entity ent = other.GetComponent<Entity>();
            ent.GetHit(ent.CurrentHP);
        }
    }
}
