using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using MapUtility;
public class LevelGenerator : MonoBehaviour
{
    public delegate void GetNavMesh();
    public delegate void LevelGenerated(LevelGenerator levelGenerator);
    public static event GetNavMesh OnGetNavmesh;
    public static event LevelGenerated OnLevelGenerated;
    [SerializeField] private HexGrid hexGrid;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject portalPrefab;
    [SerializeField] Item[] _loot;
    public Item[] Loot { get { return _loot; } }

    void Start()
    {
        StartCoroutine(GenerateDungeon());
    }

    IEnumerator GenerateDungeon()
    {
        yield return hexGrid.GenerateMap(); 
        yield return hexGrid.GeneratePaths();
        GameObject player = Instantiate(playerPrefab);
        player.transform.position = PlayerSpawnpoint.spawnpoint.transform.position;
        //TODO Correct forward dir in editor
        //player.transform.localRotation = PlayerSpawnpoint.spawnpoint.transform.localRotation;
        player.transform.localRotation = GetCorridorDirection(hexGrid.StartCell);
        if (OnGetNavmesh != null) OnGetNavmesh();
        if (OnLevelGenerated != null) OnLevelGenerated(this);
    }
    private Quaternion GetCorridorDirection(HexCell cell)
    {
        HexDirection direction = HexDirection.NE;
        for (HexDirection i = HexDirection.NE; i <= HexDirection.NW; i++)
        {
            if (cell.GetNeighbor(i) != null && cell.GetNeighbor(i).isPath)
            {
                direction = i;
                break;
            }
        }
        return Quaternion.Euler(0, 30 + 60 * (int)direction, 0);
    }
}
