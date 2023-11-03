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
            if (other.CompareTag("Player"))
            {
                ent.GetHit(ent.MaxHitpoints / 6);
                ent.transform.position = PlayerSafeSpot.playerSafeSpot.GetLastSafeSpot;
            }
            else if (other.CompareTag("Enemy"))
            {
                ent.GetHit(ent.MaxHitpoints);
            }
        }
    }
}
