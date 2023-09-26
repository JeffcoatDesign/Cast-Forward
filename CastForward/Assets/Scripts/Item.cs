using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public virtual void Interact()
    {
        Debug.Log("Interacted with: " + name);
    }
}
