using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using MapUtility;
public class LevelGenerator : MonoBehaviour
{
    public delegate void GetNavMesh();
    public delegate void LevelGenStarted();
    public delegate void LevelProgress(float currentValue, float maxValue);
    public delegate void LevelGenerated(LevelGenerator levelGenerator);
    public static event GetNavMesh OnGetNavmesh;
    public static event LevelGenStarted OnLevelGenStarted;
    public static event LevelProgress OnLevelGenProgress;
    public static event LevelGenerated OnLevelGenerated;
    [SerializeField] private HexGrid hexGrid;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject portalPrefab;
    [SerializeField] Item[] _loot;
    public string nextLevel;
    public static LevelGenerator instance;
    public Item[] Loot { get { return _loot; } }
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(GenerateDungeon());
    }

    IEnumerator GenerateDungeon()
    {
        OnLevelGenStarted?.Invoke();
        yield return hexGrid.GenerateMap();
        OnLevelGenProgress?.Invoke(1, 4);
        yield return hexGrid.GeneratePaths();
        OnLevelGenProgress?.Invoke(3, 4);
        GameObject player = Instantiate(playerPrefab);
        player.transform.position = PlayerSpawnpoint.spawnpoint.transform.position;
        OnLevelGenProgress?.Invoke(4, 4);
        player.transform.localRotation = GetCorridorDirection(hexGrid.StartCell);
        OnGetNavmesh?.Invoke();
        OnLevelGenerated?.Invoke(this);
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
