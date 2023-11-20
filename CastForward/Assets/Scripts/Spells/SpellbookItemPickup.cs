using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using SpellSystem;

public class SpellbookItemPickup : ItemPickup
{
    [SerializeField] private bool randomizeOnSpawn = false;
    [SerializeField] private Image Image;
    private void Awake()
    {
        if (randomizeOnSpawn && LevelGenerator.instance != null) {
            List<Item> spells = new (LevelGenerator.instance.Loot.Where(s => s.GetType() == typeof(Spell)).ToList());
            Item spell = spells[Random.Range(0, spells.Count)];
            item = spell;
            Initialize(item);
        }
    }
    public override void Initialize(Item newItem)
    {
        base.Initialize(newItem);
        if (newItem.inventorySprite != null)
            Image.sprite = newItem.inventorySprite;
        else
            Image.enabled = false;
    }
}
