using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class DebugPlayground : MonoBehaviour
{
    void Start()
    {
        FindFirstObjectByType<NavMeshSurface>().BuildNavMesh();
    }
}
