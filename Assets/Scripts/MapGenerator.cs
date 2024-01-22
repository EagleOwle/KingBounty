using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    #region SINGLETON
    private static MapGenerator _singleton;
    public static MapGenerator Singleton
    {
        get
        {
            if (_singleton == null)
            {
                _singleton = GameObject.FindObjectOfType<MapGenerator>();
            }
            return _singleton;
        }
    }
    #endregion

    [SerializeField] private int radius = 1;
    private const int multiplier = 6;
    public int Radius => 1 + (radius * multiplier);

    [SerializeField] private WaterGenerator waterGenerator;

    [SerializeField]private List<HexCell> cells = new List<HexCell>();
    public List<HexCell> Cells => cells;
    public void AddCell(HexCell cell)
    {
        cells.Add(cell);
        //cell.GetComponentInChildren<TextMesh>().text = cells.Count.ToString();
    }

    public void GenerateGrid()
    {
        DestroyCell();
        CreateCell(Vector3.zero);
    }

    private void CreateCell(Vector3 position)
    {
        GameObject obj = Instantiate(Resources.Load("Cell") as GameObject, transform);
        CellCreateNeighbor cellNeighbor = obj.GetComponent<CellCreateNeighbor>();
        HexCell cell = obj.GetComponent<HexCell>();
        cell.Type = CellType.Ground;
        cell.transform.position = position;
        cell.gridPosition = position;
        AddCell(cell);
        cellNeighbor.Initialise();
    }

    public void DestroyCell()
    {
        if (cells == null)
        {
            return;
        }

        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i] != null)
            {
                DestroyImmediate(cells[i].gameObject);
            }
        }

        cells = new List<HexCell>();
    }

    /// <summary>
    /// Возвращает ячейку в заданной позиции
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public HexCell GetCellByWorldPosition(Vector3 position)
    {
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].transform.position == position)
            {
                return cells[i];
            }
        }

        return null;
    }

    public void GenerateWater()
    {
        waterGenerator.GenerateWater(cells);
    }

}
