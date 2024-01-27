using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace HexWorld
{
    [CreateAssetMenu()]
    public class GameField : ScriptableObject
    {
        public  Cell[,] Cells => cells;
        private Cell[,] cells = new Cell[0, 0];

        public void CreateNewField(Vector2Int fieldSize)
        {
            cells = new Cell[fieldSize.x, fieldSize.y];
        }

        public void AddCell(Cell cell, Vector2Int index)
        {
            cells[index.x,index.y] = cell;
        }

        public void ClearAllCells()
        {
            cells = new Cell[0, 0];
        }

        public bool GetHex(Vector2 fieldPosition, out Cell cell)
        {
            cell = null;

            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {

                    if (cells[x,y].FieldPosition == fieldPosition)
                    {
                        cell = cells[x, y];
                        return true;
                    }
                }
            }

            return false;
        }

    }
}