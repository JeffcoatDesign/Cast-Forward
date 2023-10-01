using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellbookItem : ItemPickup
{
    [SerializeField] private Image Image;
    public override void Initialize(Item newItem)
    {
        base.Initialize(newItem);
        Image.sprite = newItem.inventorySprite;
    }
}
