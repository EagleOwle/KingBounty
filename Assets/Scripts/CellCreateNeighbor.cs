using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.EditorCoroutines.Editor;

public class CellCreateNeighbor : MonoBehaviour
{
    HexCell cell;
    CellNeighbor[] neighbors
    {
        get
        {
            if(cell == null)
            {
                cell = GetComponent<HexCell>();
            }

            return cell.neighbors;
        }
    }

    private void OnValidate()
    {
        for (int i = 0; i < neighbors.Length; i++)
        {
            neighbors[i].direction = (CellDirection)i;
            neighbors[i].name = neighbors[i].direction.ToString();
        }
    }

    public void Initialise()
    {
        EditorCoroutineUtility.StartCoroutine(IEWaitForSeconds(), this);
    }

    private IEnumerator IEWaitForSeconds()
    {
        yield return new EditorWaitForSeconds(Time.deltaTime);
        FindNeighbors();
        CreateNeighbors();
    }

    private void FindNeighbors()
    {
        foreach (var item in neighbors)
        {
            Vector3 position = transform.position + CellMetrics.FindPositionByDirection(item.direction);
            item.cell = MapGenerator.Singleton.GetCellByWorldPosition(position);

            if(item.cell != null)
            {
                item.cell.SetNeighbor(item.direction.Opposite(), cell);
            }

        }
    }

    private void CreateNeighbors()
    {
        for (int i = 0; i < neighbors.Length; i++)
        {
            if (MapGenerator.Singleton.Cells.Count >= MapGenerator.Singleton.Radius)
            {
                return;
            }

            if (neighbors[i].cell == null)
            {
                GameObject obj = Instantiate(Resources.Load("Cell") as GameObject, MapGenerator.Singleton.transform);
                Vector3 position = Vector3.zero;
                position = CellMetrics.FindPositionByDirection(neighbors[i].direction);
                obj.transform.position = transform.position + position;
                HexCell cell = obj.GetComponent<HexCell>();
                cell.Type = CellType.Ground;
                cell.gridPosition = obj.transform.position;
                CellCreateNeighbor cellNeighbor = obj.GetComponent<CellCreateNeighbor>();
                MapGenerator.Singleton.AddCell(cell);
                cell.neighbors[i].cell = cell;

                cellNeighbor.Initialise();
            }
        }

    }
}
