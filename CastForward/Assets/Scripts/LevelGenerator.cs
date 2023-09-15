using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using MapUtility;
public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private HexGrid hexGrid;
    [SerializeField] private GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateDungeon());
    }

    IEnumerator GenerateDungeon()
    {
        hexGrid.GenerateMap();
        yield return new WaitUntil(() => hexGrid.Cells[hexGrid.width - 1, hexGrid.height - 1] != null);
        hexGrid.GeneratePaths();
        yield return new WaitUntil(() => hexGrid.StartCell != null);
        GameObject player = Instantiate(playerPrefab);
        player.transform.position = hexGrid.StartCell.SpawnPosition;
        player.transform.localRotation = Quaternion.Euler(GetCorridorDirection(hexGrid.StartCell));
    }
    private Vector3 GetCorridorDirection(HexCell cell)
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
        return new(0, 30 + 60 * (int)direction, 0);
    }
}
