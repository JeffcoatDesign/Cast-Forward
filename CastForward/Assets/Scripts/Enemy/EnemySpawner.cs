using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private bool spawnOnLevelGenerated = true;
    [SerializeField] private float maxSpawnDistance = 5;
    private void OnEnable()
    {
        if (spawnOnLevelGenerated)
            LevelNavmesh.OnNavMeshBuilt += SpawnEnemy;
    }
    private void OnDisable()
    {
        if (spawnOnLevelGenerated)
            LevelNavmesh.OnNavMeshBuilt -= SpawnEnemy;
    }
    public void SpawnEnemy ()
    {
        GetSpawnedEnemy();
    }

    public EnemyAI GetSpawnedEnemy()
    {
        int randIndex = Random.Range(0, enemyPrefabs.Length);
        var enemyPrefab = enemyPrefabs[randIndex];
        NavMeshHit closestHit;
        if (NavMesh.SamplePosition(transform.position, out closestHit, maxSpawnDistance, 1))
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = closestHit.position;
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            enemyAI.navMeshAgent = enemy.AddComponent<NavMeshAgent>();
            enemyAI.Initialize();
            return enemyAI;
        }
        return null;
    }
}
