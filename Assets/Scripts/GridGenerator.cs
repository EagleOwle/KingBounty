using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public List<Cell> _cells = new List<Cell>();

    public void GenerateGrid()
    {
        Cell[] firstCell = new Cell[1] { new Cell(CellType.Ground, Vector2.zero) };

        foreach (var item in firstCell)
        {
            FindNeighbors(item);
        }

       // CreateNeighbor(firstCell);
    }

    private void CreateNeighbor(Cell[] cells)
    {
        foreach (var item in cells)
        {
            for (int i = 0; i < item.Neighbors.Length; i++)
            {
                if(item.Neighbors[i].cell == null)
                {

                }
            }
        }
        
    }

    private void FindNeighbors(Cell cell)
    {
        foreach (var item in cell.Neighbors)
        {
            Vector2 position = cell.Position + CellMetrics.FindGridPositionByDirection(item.direction);
            item.cell = GetCellByWorldPosition(position);

            if (item.cell != null)
            {
                item.cell.SetNeighbor(item.direction.Opposite(), cell);
            }

        }
    }

    public void DestroyCell()
    {
        throw new NotImplementedException();
    }

    public Cell GetCellByWorldPosition(Vector2 position)
    {
        for (int i = 0; i < _cells.Count; i++)
        {
            if (_cells[i].Position == position)
            {
                return _cells[i];
            }
        }

        return null;
    }

    [System.Serializable]
    public class Cell
    {
        public Cell (CellType type, Vector2 gridPosition)
        {
            Type = type;
            Position = gridPosition;

           Neighbors = new CellNeighbor[6];

            for (int i = 0; i < Neighbors.Length; i++)
            {
                Neighbors[i].direction = (CellDirection)i;
                Neighbors[i].name = Neighbors[i].direction.ToString();
            }

        }

        public CellType Type;
        public Vector2 Position;
        public CellNeighbor[] Neighbors;

        public void SetNeighbor(CellDirection direction, Cell cell)
        {
            Neighbors[(int)direction].cell = cell;
        }

    }

    [System.Serializable]
    public class CellNeighbor
    {
        public string name;
        public CellDirection direction;
        public Cell cell;
    }


    

}
