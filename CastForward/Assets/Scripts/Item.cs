using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string ItemName;
    public GameObject itemPrefab;
    public Mesh mesh;
    public Material material;
    public Sprite inventorySprite;

    public virtual void Interact()
    {
        Debug.Log("Interacted with: " + name);
    }
    public virtual void SpawnItem(Vector3 position)
    {
        ItemPickup itemObject = Instantiate(itemPrefab).GetComponent<ItemPickup>();
        itemObject.transform.SetPositionAndRotation(position, Quaternion.LookRotation(position, Vector3.up));
        itemObject.Initialize(this);
    }
}
