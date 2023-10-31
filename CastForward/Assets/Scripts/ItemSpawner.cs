using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPosTransform;
    private Item[] _itemsToSpawn;
    private void OnEnable()
    {
        LevelGenerator.OnLevelGenerated += SpawnItem;
    }
    private void OnDisable()
    {
        LevelGenerator.OnLevelGenerated -= SpawnItem;
    }
    void SpawnItem (LevelGenerator levelGenerator)
    {
        _itemsToSpawn = levelGenerator.Loot;
        if (_itemsToSpawn.Length > 0)
        {
            int index = Random.Range(0, _itemsToSpawn.Length);
            _itemsToSpawn[index].SpawnItem(spawnPosTransform.position);
        }
    }
}
