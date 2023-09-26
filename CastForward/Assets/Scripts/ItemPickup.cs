using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] Item item;
    public override void Interact()
    {
        item.Interact();
        Destroy(gameObject);
    }
}
