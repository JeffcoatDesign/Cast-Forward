using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using MapUtility;
public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private HexGrid hexGrid;
    [SerializeField] private GameObject playerPrefab;
    private bool isGenerating = true;
// TODO: MAKE BETTER
    void Start()
    {
        StartCoroutine(GenerateDungeon());
    }

    IEnumerator GenerateDungeon()
    {
        yield return hexGrid.GenerateMap(); 
        yield return hexGrid.GeneratePaths();
        GameObject player = Instantiate(playerPrefab);
        player.transform.position = hexGrid.StartCell.SpawnPosition;
        player.transform.localRotation = Quaternion.Euler(GetCorridorDirection(hexGrid.StartCell));
        isGenerating = false;
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
    private void OnGUI()
    {
        if (isGenerating)
            GUI.TextArea(new Rect(1, 1, 200, 30), "Level Generating");
    }
}
