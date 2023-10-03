using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum HexRoomType
{
    Normal,
    Spawn,
    Portal,
    Lore
}

public class HexRoom : MonoBehaviour
{
    public int weight = 1;
    public HexRoomType hexRoomType = HexRoomType.Normal;
    [SerializeField] private bool[] passages = new bool [6];
    public bool[] Passages { get { return passages; } }
}
