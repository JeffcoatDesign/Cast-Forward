using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTrapTrigger : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> spawns;
    public UnityEvent OnSpawnEnemies;
    public UnityEvent OnAllEnemiesDied;
    private bool hasSpawnedEnemies = false;
    private int enemyCount;
    private void Start()
    {
        OnSpawnEnemies ??= new();
        OnAllEnemiesDied ??= new();
    }
    public void SpawnEnemies ()
    {
        if (hasSpawnedEnemies) { return; }
        OnSpawnEnemies.Invoke();
        hasSpawnedEnemies = true;
        foreach(var spawner in spawns)
        {
            if (spawner != null)
            {
                EnemyAI enemy = spawner.GetSpawnedEnemy();
                if (enemy != null)
                {
                    enemy.GetComponent<EnemyEntity>().OnDeath += RemoveEnemy;
                    enemyCount++;
                }
            }
        }
    }

    private void RemoveEnemy()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            OnAllEnemiesDied.Invoke();
        }
    }
}
