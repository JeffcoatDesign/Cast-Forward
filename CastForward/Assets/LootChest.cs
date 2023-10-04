using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootChest : Interactable
{
    private bool isOpen = false;
    public Item lootItem;
    public Transform lidTransform;
    public Transform spawnpoint;
    void Start()
    {
        if (lootItem == null)
        {
            LevelGenerator levelGenerator = FindFirstObjectByType<LevelGenerator>();
            if (levelGenerator != null)
            {
                int index = Random.Range(0, levelGenerator.Loot.Length);
                lootItem = levelGenerator.Loot[index];
            }
        }
    }
    public override void Interact()
    {
        if (lootItem == null) return;
        if (!isOpen)
        {
            lidTransform.Rotate(new(-110, 0, 0));
            lootItem.SpawnItem(spawnpoint.position);
            gameObject.tag = "Untagged";
            Destroy(this);
        }
    }
}
