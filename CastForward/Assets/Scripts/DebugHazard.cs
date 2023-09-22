using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHazard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Entity ent = other.GetComponent<Entity>();
            if (ent) ent.GetHit(1);
        }
    }
}
