using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnpoint : MonoBehaviour
{
    public static PlayerSpawnpoint spawnpoint;
    private void Awake()
    {
        spawnpoint = this;
    }
}
