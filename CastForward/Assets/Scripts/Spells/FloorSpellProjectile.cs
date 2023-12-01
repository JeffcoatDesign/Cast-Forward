using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SpellSystem
{
    public class FloorSpellProjectile : SpellProjectile
    {
        [SerializeField] private Transform searchPoint;
        [SerializeField] private float maxDistance = 2;
        private void Start()
        {
            if (searchPoint == null) {
                searchPoint = transform;
            }
            Vector3 pos = searchPoint.position;
            NavMeshHit navMeshHit = new NavMeshHit();
            if (NavMesh.SamplePosition(pos, out navMeshHit, maxDistance, 1))
            {
                pos = navMeshHit.position;
                SpawnImpact(pos);
            }
            else
                Debug.Log("No position close enough");
            Destroy(gameObject);
        }
    }
}